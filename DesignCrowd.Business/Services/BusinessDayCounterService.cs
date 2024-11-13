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

    public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
    {
        if (secondDate <= firstDate)
        {
            _logger.LogDebug("First date is greater then second date");
            return 0;
        }

        int weekdays = 0;

        for (var date = firstDate.AddDays(1); date < secondDate; date = date.AddDays(1))
        {
            if (IsBusinessDay(date))
            {
                weekdays++;
            }
        }

        return weekdays;
    }

    public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
    {
        // Calculate weekdays as before
        int weekdays = WeekdaysBetweenTwoDates(firstDate, secondDate);

        // Subtract holidays within the date range
        foreach (var holiday in publicHolidays)
        {
            if (holiday > firstDate && holiday < secondDate && IsBusinessDay(holiday))
            {
                weekdays--;
            }
        }

        return weekdays;
    }

    public static int BusinessDaysBetweenTwoDates(DateTime startDate, DateTime endDate, IEnumerable<PublicHolidayRule> holidayRules)
    {
        if (endDate <= startDate)
        {
            return 0;
        }

        int businessDays = 0;

        for (DateTime date = startDate.AddDays(1); date < endDate; date = date.AddDays(1))
        {
            if (IsBusinessDay(date, holidayRules))
            {
                businessDays++;
            }
        }

        return businessDays;
    }

    private static bool IsBusinessDay(DateTime date, IEnumerable<PublicHolidayRule> holidayRules)
    {
        return date.DayOfWeek != DayOfWeek.Saturday &&
               date.DayOfWeek != DayOfWeek.Sunday &&
               !holidayRules.Any(rule => rule.IsHoliday(date));
    }

    private static bool IsBusinessDay(DateTime date)
    {
        return date.DayOfWeek != DayOfWeek.Saturday &&
               date.DayOfWeek != DayOfWeek.Sunday;
    }
}
