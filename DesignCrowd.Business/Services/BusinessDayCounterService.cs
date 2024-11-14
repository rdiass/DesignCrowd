using DesignCrowd.Business.Interfaces;
using DesignCrowd.Data.Abstraction;
using Microsoft.Extensions.Logging;

namespace DesignCrowd.Business.Services;

public class BusinessDayCounterService : IBusinessDayCounterService
{
    private readonly ILogger<BusinessDayCounterService> _logger;

    /// <summary>
    /// Contructor
    /// </summary>
    /// <param name="logger"></param>
    public BusinessDayCounterService(ILogger<BusinessDayCounterService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Calculates the number of weekdays between two dates
    /// </summary>
    /// <param name="firstDate">The first date in the range.</param>
    /// <param name="secondDate">The second date in the range.</param>
    /// <param name="holidayRules">Optional collection of rules to identify public holidays (defaults to empty list).</param>
    /// <returns>The number of weekdays between the provided dates, excluding public holidays.</returns>
    public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IEnumerable<PublicHolidayRule>? holidayRules = null)
    {
        if (secondDate <= firstDate)
        {
            _logger.LogDebug("Invalid date range: Second date must be after first date. Returning 0 weekdays.");
            return 0;
        }

        int weekdays = 0;

        // Iterate through each day between first and second date (exclusive)
        for (var date = firstDate.AddDays(1); date < secondDate; date = date.AddDays(1))
        {
            if (IsBusinessDay(date, holidayRules ?? []))
            {
                weekdays++;
            }
        }

        return weekdays;
    }

    /// <summary>
    /// Calculates the number of weekdays between two given dates, considering provided public holidays.
    /// </summary>
    /// <param name="firstDate">The first date in the range.</param>
    /// <param name="secondDate">The second date in the range.</param>
    /// <param name="publicHolidays">A list of specific public holiday dates.</param>
    /// <returns>The number of weekdays between the provided dates, excluding weekends and the listed public holidays.</returns>
    public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
    {
        // Get weekdays excluding weekends
        int weekdays = WeekdaysBetweenTwoDates(firstDate, secondDate);

        // Remove public holidays that fall on weekdays within the date range        
        foreach (var holiday in publicHolidays)
        {
            if (holiday > firstDate && holiday < secondDate && IsBusinessDay(holiday, []))
            {
                weekdays--;
            }
        }

        return weekdays;
    }

    /// <summary>
    /// Calculates the number of weekdays between two dates, considering provided public holiday rules.
    /// </summary>
    /// <param name="firstDate">The first date in the range.</param>
    /// <param name="secondDate">The second date in the range.</param>
    /// <param name="publicHolidays">A list of specific public holiday dates.</param>
    /// <returns>The number of weekdays between the provided dates, excluding weekends and holidays defined by the rules.</returns>
    public int BusinessDaysBetweenTwoDates(DateTime startDate, DateTime endDate, IEnumerable<PublicHolidayRule> holidayRules)
    {
        return WeekdaysBetweenTwoDates(startDate, endDate, holidayRules);
    }

    /// <summary>
    /// Checks if a given date is considered a business day.
    /// </summary>
    /// <param name="date">The date to be evaluated.</param>
    /// <param name="holidayRules">Optional collection of rules to identify public holidays (defaults to empty list).</param>
    /// <returns>True if the date is a weekday (not Saturday or Sunday) and not a public holiday based on the provided rules; False otherwise.</returns>
    private static bool IsBusinessDay(DateTime date, IEnumerable<PublicHolidayRule> holidayRules)
    {
        return date.DayOfWeek != DayOfWeek.Saturday &&
               date.DayOfWeek != DayOfWeek.Sunday &&
               !holidayRules.Any(rule => rule.IsHoliday(date));
    }
}
