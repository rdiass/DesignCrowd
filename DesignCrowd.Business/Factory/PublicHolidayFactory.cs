using DesignCrowd.Business.Interfaces;
using DesignCrowd.Business.Models;

namespace DesignCrowd.Business.Factory;

public class PublicHolidayFactory : IPublicHolidayFactory
{
    /// <summary>
    /// Creates a PublicHolidayRule object based on the provided rule type and parameters.
    /// </summary>
    /// <param name="ruleType">The type of public holiday rule to create (e.g., "FixedDate", "Floating").</param>
    /// <param name="fixedDate">Optional fixed date for the holiday rule (used for "FixedDate" rule type).</param>
    /// <param name="dayOfWeek">Optional day of the week for the holiday rule (used for "Floating" rule type).</param>
    /// <param name="month">Optional month for the holiday rule (used for "Floating" rule type).</param>
    /// <param name="weekOccurrence">Optional week occurrence for the holiday rule (used for "Floating" rule type).</param>
    /// <returns>A PublicHolidayRule object representing the specified rule type and parameters.</returns>
    /// <exception cref="ArgumentException">Thrown if required parameters are missing or the rule type is invalid.</exception>
    public IPublicHolidayRule CreateHolidayRule(string ruleType, DateTime? fixedDate = null, DayOfWeek? dayOfWeek = null, int? month = null, int? weekOccurrence = null)
    {
        switch (ruleType)
        {
            case "FixedDate":
                // Validate and create FixedDateHolidayRule
                if (fixedDate.HasValue)
                {
                    return new FixedDateHolidayRule(fixedDate.Value);
                }
                throw new ArgumentException("fixedDate is required for FixedDate rule type");

            case "Floating":
                // Validate and create FloatingHolidayRule
                if (dayOfWeek.HasValue && month.HasValue && weekOccurrence.HasValue)
                {
                    return new FloatingHolidayRule(dayOfWeek.Value, month.Value, weekOccurrence.Value);
                }
                throw new ArgumentException("dayOfWeek, month, and weekOccurrence are required for Floating rule type");

            default:
                throw new ArgumentException("Invalid rule type provided");
        }
    }
}
