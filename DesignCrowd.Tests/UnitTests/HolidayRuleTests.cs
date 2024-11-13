using DesignCrowd.Data.Factory;
using FluentAssertions;

namespace DesignCrowd.Tests.UnitTests;

public class HolidayRuleTests
{
    private readonly PublicHolidayFactory _sut;

    public HolidayRuleTests()
    {
        _sut = new PublicHolidayFactory();
    }

    [Fact]
    public void FixedDateHolidayRule_IsHoliday_ReturnsTrueForFixedDate()
    {
        // Arrange
        var fixedDate = new DateTime(2024, 12, 25);
        var holidayRule = _sut.CreateHolidayRule("FixedDate", fixedDate);
            
        // Act
        var isHoliday = holidayRule.IsHoliday(fixedDate);

        // Assert
        isHoliday.Should().BeTrue();
    }

    [Fact]
    public void FixedDateHolidayRule_IsHoliday_ReturnsFalseForDifferentDate()
    {
        // Arrange
        var fixedDate = new DateTime(2024, 12, 25);
        var holidayRule = _sut.CreateHolidayRule("FixedDate", fixedDate);

        // Act
        var isHoliday = holidayRule.IsHoliday(new DateTime(2024, 12, 26));

        // Assert
        isHoliday.Should().BeFalse();
    }

    [Fact]
    public void FloatingHolidayRule_IsHoliday_ReturnsTrueForCorrectDate()
    {
        // Arrange
        var holidayRule = _sut.CreateHolidayRule("Floating", null, DayOfWeek.Monday, 1, 1); // New Year's Day on the first Monday of January

        // Act
        var isHoliday = holidayRule.IsHoliday(new DateTime(2024, 1, 1)); // Assuming 1st Jan 2024 is a Monday

        // Assert
        isHoliday.Should().BeTrue();
    }

    [Fact]
    public void FloatingHolidayRule_IsHoliday_ReturnsFalseForIncorrectDate()
    {
        // Arrange
        var holidayRule = _sut.CreateHolidayRule("Floating", null, DayOfWeek.Monday, 1, 1);
        
        // Act
        var isHoliday = holidayRule.IsHoliday(new DateTime(2024, 1, 6)); // Assuming 1st Jan 2024 is a Sunday

        // Assert
        isHoliday.Should().BeFalse();
    }
}
