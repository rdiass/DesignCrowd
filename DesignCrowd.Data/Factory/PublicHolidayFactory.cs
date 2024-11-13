using DesignCrowd.Data.Abstraction;
using DesignCrowd.Data.Models;

namespace DesignCrowd.Data.Factory;

public class PublicHolidayFactory : IPublicHolidayFactory
{
    public PublicHolidayRule CreateHolidayRule(string ruleType, DateTime? fixedDate = null, DayOfWeek? dayOfWeek = null, int? month = null, int? weekOccurrence = null)
    {
        switch (ruleType)
        {
            case "FixedDate":
                return new FixedDateHolidayRule(fixedDate.Value);
            case "Floating":
                return new FloatingHolidayRule(dayOfWeek.Value, month.Value, weekOccurrence.Value);
            default:
                throw new ArgumentException("Invalid rule type");
        }
    }
}
