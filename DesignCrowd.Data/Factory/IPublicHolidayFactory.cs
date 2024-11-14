using DesignCrowd.Data.Abstraction;

namespace DesignCrowd.Data.Factory;

public interface IPublicHolidayFactory
{
    PublicHolidayRule CreateHolidayRule(string ruleType, DateTime? fixedDate = null, DayOfWeek? dayOfWeek = null, int? month = null, int? weekOccurrence = null);
}
