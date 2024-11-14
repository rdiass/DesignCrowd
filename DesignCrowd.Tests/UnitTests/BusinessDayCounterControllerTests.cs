using DesignCrowd.Api.Controllers;
using DesignCrowd.Business.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DesignCrowd.Tests.IntegrationTests;

public class BusinessDayCounterControllerTests
{

    private readonly Mock<IBusinessDayCounterService> _mockBusinessDayCounterService;
    private readonly Mock<ILogger<BusinessDayCounterController>> _logger;
    private readonly BusinessDayCounterController _controller;

    public BusinessDayCounterControllerTests()
    {
        _mockBusinessDayCounterService = new Mock<IBusinessDayCounterService>();
        _logger = new Mock<ILogger<BusinessDayCounterController>>();
        _mockBusinessDayCounterService.Setup(
            s => s.WeekdaysBetweenTwoDates(It.IsAny<DateTime>(), It.IsAny<DateTime>(), null))
        .Throws<Exception>();

        _controller = new BusinessDayCounterController(_logger.Object, _mockBusinessDayCounterService.Object);
    }

    [Fact]
    public void GetWeekdaysBetweenTwoDates_InvalidDateRange_ReturnsBadRequest()
    {
        // Arrange
        var firstDate = new DateTime(2023, 11, 10);
        var secondDate = new DateTime(2023, 11, 25); // Second date is before first date

        // Act
        var result = _controller.GetWeekdaysBetweenTwoDates(firstDate, secondDate);

        // Assert
        result.Should().BeOfType<ObjectResult>();

        var badRequestObjectResult = (ObjectResult)result;
        badRequestObjectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);        
    }
}