using DesignCrowd.Business.Interfaces;

namespace DesignCrowd.Business.Models;

public class FloatingHolidayRule : IPublicHolidayRule
{
    private readonly DayOfWeek _dayOfWeek;
    private readonly int _month;
    private readonly int _weekOccurrence;

    /// <summary>
    /// Represents a public holiday rule that occurs on a specific day of the week within a specific month and week occurrence.
    /// </summary>
    /// <param name="dayOfWeek">The day of the week the holiday falls on (e.g., Monday, Tuesday).</param>
    /// <param name="month">The month the holiday occurs in (1-12).</param>
    /// <param name="weekOccurrence">The occurrence of the specified day of the week within the month (e.g., 1st, 2nd, 3rd, 4th, or Last).</param>
    public FloatingHolidayRule(DayOfWeek dayOfWeek, int month, int weekOccurrence)
    {
        _dayOfWeek = dayOfWeek;
        _month = month;
        _weekOccurrence = weekOccurrence;
    }

    /// <summary>
    /// Checks if a given date falls on this floating holiday.
    /// </summary>
    /// <param name="date">The date to be evaluated.</param>
    /// <returns>True if the date is the specified day of the week within the specified month and week occurrence; False otherwise.</returns>
    public bool IsHoliday(DateTime date)
    {
        return date.DayOfWeek == _dayOfWeek &&
               date.Month == _month &&
               GetWeekOccurrence(date) == _weekOccurrence;
    }

    /// <summary>
    /// Calculates the week occurrence (e.g., 1st, 2nd, 3rd, 4th, or Last) of a given date within its month.
    /// </summary>
    /// <param name="date">The date for which to calculate the week occurrence.</param>
    /// <returns>The week occurrence of the date within its month (1-4 or Last).</returns>
    private static int GetWeekOccurrence(DateTime date)
    {
        // Calculate the day of the week for the first day of the month
        int firstDayOfMonth = (int)new DateTime(date.Year, date.Month, 1).DayOfWeek;

        // Calculate the day of the month for the provided date
        int dayOfMonth = date.Day;

        // Calculate the week occurrence based on the difference between the day of the month and the first day of the week (adjusted for 0-based indexing)
        return (dayOfMonth + (7 - firstDayOfMonth)) / 7;
    }
}
