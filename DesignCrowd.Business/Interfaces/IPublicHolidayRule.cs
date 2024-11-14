namespace DesignCrowd.Business.Interfaces;

public interface IPublicHolidayRule
{
    public abstract bool IsHoliday(DateTime date);
}
