using DesignCrowd.Data.Factory;
using DesignCrowd.Data.Models;
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
        var holidayRule = _sut.CreateHolidayRule("Floating", null, DayOfWeek.Monday, 1, 1);

        // Act
        var isHoliday = holidayRule.IsHoliday(new DateTime(2024, 1, 1));

        // Assert
        isHoliday.Should().BeTrue();
    }

    [Fact]
    public void FloatingHolidayRule_IsHoliday_ReturnsFalseForIncorrectDate()
    {
        // Arrange
        var holidayRule = _sut.CreateHolidayRule("Floating", null, DayOfWeek.Monday, 1, 1);
        
        // Act
        var isHoliday = holidayRule.IsHoliday(new DateTime(2024, 1, 6));

        // Assert
        isHoliday.Should().BeFalse();
    }

    [Fact]
    public void CreateFixedDateRule_ValidInput_ReturnsFixedDateRule()
    {
        // Arrange
        var fixedDate = new DateTime(2023, 12, 25);

        // Act
        var rule = _sut.CreateHolidayRule("FixedDate", fixedDate);

        // Assert
        rule.Should().BeOfType<FixedDateHolidayRule>();
    }

    [Fact]
    public void CreateFixedDateRule_NullFixedDate_ThrowsArgumentException()
    {
        // Act & Assert
        Action act = () => _sut.CreateHolidayRule("FixedDate", null);
        act.Should().Throw<ArgumentException>().WithMessage("fixedDate is required for FixedDate rule type");
    }

    [Fact]
    public void CreateFloatingRule_ValidInput_ReturnsFloatingRule()
    {
        // Arrange
        var dayOfWeek = DayOfWeek.Monday;
        var month = 1;
        var weekOccurrence = 3;

        // Act
        var rule = _sut.CreateHolidayRule("Floating", null, dayOfWeek, month, weekOccurrence);

        // Assert
        rule.Should().BeOfType<FloatingHolidayRule>();
    }

    [Fact]
    public void CreateFloatingRule_NullDayOfWeek_ThrowsArgumentException()
    {
        // Act & Assert
        Action act = () => _sut.CreateHolidayRule("Floating", null, null, 1, 3);
        act.Should().Throw<ArgumentException>().WithMessage("dayOfWeek, month, and weekOccurrence are required for Floating rule type");
    }

    [Fact]
    public void CreateInvalidRuleType_ThrowsArgumentException()
    {
        // Act & Assert
        Action act = () => _sut.CreateHolidayRule("InvalidType");
        act.Should().Throw<ArgumentException>().WithMessage("Invalid rule type provided");
    }
}
