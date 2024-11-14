using DesignCrowd.Business.Interfaces;

namespace DesignCrowd.Data.Models;

public class FixedDateHolidayRule : IPublicHolidayRule
{
    private readonly DateTime _fixedDate;

    public FixedDateHolidayRule(DateTime fixedDate)
    {
        _fixedDate = fixedDate;
    }

    public bool IsHoliday(DateTime date) => date.Date == _fixedDate.Date;
}
