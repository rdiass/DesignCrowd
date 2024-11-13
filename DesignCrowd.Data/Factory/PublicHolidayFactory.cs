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
                if (fixedDate.HasValue)
                {
                    return new FixedDateHolidayRule(fixedDate.Value);
                }
                throw new ArgumentException("fixedDate is null");
            case "Floating":
                if (dayOfWeek.HasValue && month.HasValue && weekOccurrence.HasValue)
                {
                    return new FloatingHolidayRule(dayOfWeek.Value, month.Value, weekOccurrence.Value);
                }
                throw new ArgumentException("dayOfWeek is null");
            default:
                throw new ArgumentException("Invalid rule type");
        }
    }
}
