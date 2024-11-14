using DesignCrowd.Business.Interfaces;
using DesignCrowd.Data.Abstraction;
using Microsoft.Extensions.Logging;

namespace DesignCrowd.Business.Services;

public class BusinessDayCounterService : IBusinessDayCounterService
{
    private readonly ILogger<BusinessDayCounterService> _logger;

    public BusinessDayCounterService(ILogger<BusinessDayCounterService> logger)
    {
        _logger = logger;
    }

    public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IEnumerable<PublicHolidayRule>? holidayRules = null)
    {
        if (secondDate <= firstDate)
        {
            _logger.LogDebug("First date is greater then second date");
            return 0;
        }

        int weekdays = 0;

        for (var date = firstDate.AddDays(1); date < secondDate; date = date.AddDays(1))
        {
            if (IsBusinessDay(date, holidayRules ?? []))
            {
                weekdays++;
            }
        }

        return weekdays;
    }

    public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
    {
        // Get weekdays
        int weekdays = WeekdaysBetweenTwoDates(firstDate, secondDate);

        // Removing holidays that is business day between two dates
        foreach (var holiday in publicHolidays)
        {
            if (holiday > firstDate && holiday < secondDate && IsBusinessDay(holiday, []))
            {
                weekdays--;
            }
        }

        return weekdays;
    }

    public int BusinessDaysBetweenTwoDates(DateTime startDate, DateTime endDate, IEnumerable<PublicHolidayRule> holidayRules)
    {
        return WeekdaysBetweenTwoDates(startDate, endDate, holidayRules);
    }

    private static bool IsBusinessDay(DateTime date, IEnumerable<PublicHolidayRule> holidayRules)
    {
        return date.DayOfWeek != DayOfWeek.Saturday &&
               date.DayOfWeek != DayOfWeek.Sunday &&
               !holidayRules.Any(rule => rule.IsHoliday(date));
    }
}
