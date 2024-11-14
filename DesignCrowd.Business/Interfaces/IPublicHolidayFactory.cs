namespace DesignCrowd.Business.Interfaces;

public interface IPublicHolidayFactory
{
    IPublicHolidayRule CreateHolidayRule(string ruleType, DateTime? fixedDate = null, DayOfWeek? dayOfWeek = null, int? month = null, int? weekOccurrence = null);
}
