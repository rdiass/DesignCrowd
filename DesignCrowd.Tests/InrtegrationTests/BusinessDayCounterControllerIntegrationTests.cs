using DesignCrowd.Api.Controllers;
using DesignCrowd.Business.Models.Request;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DesignCrowd.Tests.IntegrationTests;

public class BusinessDayCounterControllerIntegrationTests : IClassFixture<WebApplicationFactory<BusinessDayCounterController>>
{
    private readonly WebApplicationFactory<BusinessDayCounterController> _factory;

    public BusinessDayCounterControllerIntegrationTests(WebApplicationFactory<BusinessDayCounterController> factory) => _factory = factory;

    [Theory]
    [InlineData("2013-10-07", "2013-10-09", 1)]
    [InlineData("2013-10-05", "2013-10-14", 5)]
    [InlineData("2013-10-07", "2014-01-01", 61)]
    public async Task Giver_ValidDates_GetWeekdaysBetweenTwoDates_ReturnsCorrectResult(string firstDateString, string secondDateString, int expectedBusinessDays)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/weekdays?firstDate={firstDateString}&secondDate={secondDateString}");
        response.EnsureSuccessStatusCode();       

        var content = await response.Content.ReadAsStringAsync();
        var businessDays = int.Parse(content);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        businessDays.Should().Be(expectedBusinessDays);        
    }

    [Theory]
    [InlineData("2013-10-07", "2013-10-05")]
    public async Task Given_InvalidDates_GetWeekdaysBetweenTwoDates_ReturnsBadRequestResult(string firstDateString, string secondDateString)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/weekdays?firstDate={firstDateString}&secondDate={secondDateString}");        
        var content = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(content);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        problemDetails?.Detail.Should().Be("Second date must be after first date");
    }

    [Theory]
    [InlineData("2013-10-07", "2013-10-09", new string[] { }, 1)]  // No holidays, same as GetWeekdaysBetweenTwoDates
    [InlineData("2013-10-05", "2013-10-14", new string[] { "2013-10-10" }, 4)]  // One holiday in between
    [InlineData("2013-10-07", "2014-01-01", new string[] { "2013-12-25", "2013-12-26" }, 59)]  // Holidays within the range
    public async Task Given_ValidDatesAndHolidays_GetBusinessDaysBetweenTwoDatesWithHolidays_ReturnsCorrectResult(
    string firstDateString, string secondDateString, string[] holidays, int expectedBusinessDays)
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new BusinessDayCalculationRequest
        {
            FirstDate = DateTime.Parse(firstDateString),
            SecondDate = DateTime.Parse(secondDateString),
            Holidays = holidays.Select(h => DateTime.Parse(h)).ToList()
        };
        
        // Act
        var response = await client.PostAsJsonAsync("/api/businessdays", request);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var businessDays = int.Parse(responseContent);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        businessDays.Should().Be(expectedBusinessDays);
    }

    [Theory]
    [InlineData("2013-10-07", "2013-10-05")]
    public async Task Given_InvalidDates_GetBusinessDaysBetweenTwoDatesWithHolidays_ReturnsBadRequestResult(string firstDateString, string secondDateString)
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new BusinessDayCalculationRequest
        {
            FirstDate = DateTime.Parse(firstDateString),
            SecondDate = DateTime.Parse(secondDateString),
            Holidays = new List<DateTime>()
        };

        // Act
        var response = await client.PostAsJsonAsync("/api/businessdays", request);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        var contentString = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(contentString);
        problemDetails?.Detail.Should().Be("Second date must be after first date");
    }
}