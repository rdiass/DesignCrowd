namespace DesignCrowd.Data.Abstraction;

/// <summary>
/// Factory Pattern
/// </summary>
public abstract class PublicHolidayRule
{
    public abstract bool IsHoliday(DateTime date);
}
