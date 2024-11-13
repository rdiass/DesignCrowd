using DesignCrowd.Data.Abstraction;

namespace DesignCrowd.Data.Models;

public class FixedDateHolidayRule : PublicHolidayRule
{
    private readonly DateTime _fixedDate;

    public FixedDateHolidayRule(DateTime fixedDate)
    {
        _fixedDate = fixedDate;
    }

    public override bool IsHoliday(DateTime date) => date.Date == _fixedDate.Date;
}
