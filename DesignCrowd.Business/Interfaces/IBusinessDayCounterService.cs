namespace DesignCrowd.Business.Interfaces;

public interface IBusinessDayCounterService
{
    int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate);
    int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays);
}
