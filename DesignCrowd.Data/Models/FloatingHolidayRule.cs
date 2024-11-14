using DesignCrowd.Data.Abstraction;

namespace DesignCrowd.Data.Models;

public class FloatingHolidayRule : PublicHolidayRule
{
    private readonly DayOfWeek _dayOfWeek;
    private readonly int _month;
    private readonly int _weekOccurrence;

    public FloatingHolidayRule(DayOfWeek dayOfWeek, int month, int weekOccurrence)
    {
        _dayOfWeek = dayOfWeek;
        _month = month;
        _weekOccurrence = weekOccurrence;
    }

    public override bool IsHoliday(DateTime date)
    {
        return date.DayOfWeek == _dayOfWeek &&
               date.Month == _month &&
               GetWeekOccurrence(date) == _weekOccurrence;
    }

    private static int GetWeekOccurrence(DateTime date)
    {
        int firstDayOfMonth = (int)new DateTime(date.Year, date.Month, 1).DayOfWeek;
        int dayOfMonth = date.Day;
        return (dayOfMonth + (7 - firstDayOfMonth)) / 7;
    }
}
