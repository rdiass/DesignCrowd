namespace DesignCrowd.Business.Interfaces;

public interface IBusinessDayCounterService
{
    int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IEnumerable<IPublicHolidayRule>? holidayRules = null);
    int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays);
}
