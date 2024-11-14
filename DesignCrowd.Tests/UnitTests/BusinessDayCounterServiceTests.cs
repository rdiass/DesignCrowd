using DesignCrowd.Business.Interfaces;
using DesignCrowd.Business.Services;
using DesignCrowd.Data.Factory;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace DesignCrowd.Tests.UnitTests;

public class BusinessDayCounterServiceTests
{
    private readonly BusinessDayCounterService _sut;
    private readonly Mock<ILogger<BusinessDayCounterService>> _logger;
    private readonly PublicHolidayFactory _publicHolidayFactory;

    public BusinessDayCounterServiceTests()
    {
        _logger = new Mock<ILogger<BusinessDayCounterService>>();
        _sut = new BusinessDayCounterService(_logger.Object);
        _publicHolidayFactory = new PublicHolidayFactory();
    }

    [Theory]
    [InlineData("2013-10-07", "2013-10-09", 1)]
    [InlineData("2013-10-05", "2013-10-14", 5)]
    [InlineData("2013-10-07", "2014-01-01", 61)]
    [InlineData("2013-10-07", "2013-10-05", 0)]
    public void WeekdaysBetweenTwoDates_ShouldReturnCorrectWeekdays(string firstDateString, string secondDateString, int expectedWeekdays)
    {
        // Arrange
        var firstDate = DateTime.Parse(firstDateString);
        var secondDate = DateTime.Parse(secondDateString);

        // Act
        int actualWeekdays = _sut.WeekdaysBetweenTwoDates(firstDate, secondDate);

        // Assert
        actualWeekdays.Should().Be(expectedWeekdays);
    }

    [Theory]
    [InlineData("2013-10-07", "2013-10-09", 1)]
    [InlineData("2013-12-24", "2013-12-27", 0)]
    [InlineData("2013-10-07", "2014-01-01", 59)]    
    public void BusinessDaysBetweenTwoDates_ShouldReturnCorrectBusinessDays(string firstDateString, string secondDateString, int expectedBusinessDays)
    {
        // Arrange
        var holidayStrings = new[] { "2013-12-25", "2013-12-26", "2014-01-01" };
        var firstDate = DateTime.Parse(firstDateString);
        var secondDate = DateTime.Parse(secondDateString);
        var holidays = holidayStrings.Select(h => DateTime.Parse(h)).ToList();

        // Act
        int actualBusinessDays = _sut.BusinessDaysBetweenTwoDates(firstDate, secondDate, holidays);

        // Assert
        actualBusinessDays.Should().Be(expectedBusinessDays);
    }

    [Theory]
    [InlineData("2013-10-07", "2013-10-09", 1)]
    [InlineData("2013-12-24", "2013-12-27", 0)]
    [InlineData("2013-10-07", "2014-01-01", 59)]    
    [InlineData("2013-10-07", "2013-10-05", 0)]
    public void CalculateBusinessDays_ShouldReturnCorrectBusinessDays(string startDateString, string endDateString, int expectedBusinessDays)
    {
        // Arrange
        DateTime startDate = DateTime.Parse(startDateString);
        DateTime endDate = DateTime.Parse(endDateString);

        var holidays = new[] { "2013-12-25", "2013-12-26" };
        var holidayRules = holidays.Select(h => _publicHolidayFactory.CreateHolidayRule("FixedDate", DateTime.Parse(h))).ToList(); // For simple fixed date holidays

        // Act
        int actualBusinessDays = _sut.BusinessDaysBetweenTwoDates(startDate, endDate, holidayRules);

        // Assert
        actualBusinessDays.Should().Be(expectedBusinessDays);
    }

    [Theory]
    [InlineData("2023-12-31", "2024-01-07", 4)]
    public void Given_NewYearsDay_On_Monday_CalculateBusinessDays_ShouldReturnCorrectBusinessDays(string startDateString, string endDateString, int expectedBusinessDays)
    {
        // Arrange
        DateTime startDate = DateTime.Parse(startDateString);
        DateTime endDate = DateTime.Parse(endDateString);

        // New Year's Day on January 1st every year, unless that is a Saturday or Sunday, in which case the holiday is the next Monday
        // Adding 1 day of holiday in the first week of January
        var holidayRules = new List<IPublicHolidayRule>
        {
            _publicHolidayFactory.CreateHolidayRule("Floating", null, DayOfWeek.Monday, 1, 1)
        };

        var holidays = new[] { "2013-12-25", "2013-12-26" };
        var fixedHolidayRules = holidays.Select(h => _publicHolidayFactory.CreateHolidayRule("FixedDate", DateTime.Parse(h))).ToList(); // For simple fixed date holidays

        holidayRules.AddRange(fixedHolidayRules);

        // Act
        int actualBusinessDays = _sut.BusinessDaysBetweenTwoDates(startDate, endDate, holidayRules);

        // Assert
        actualBusinessDays.Should().Be(expectedBusinessDays);
    }
}