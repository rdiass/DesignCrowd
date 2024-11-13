using DesignCrowd.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace DesignCrowd.Tests.IntegrationTests;

public class BusinessDayCounterControllerIntegrationTests : IClassFixture<WebApplicationFactory<BusinessDayCounterController>>
{
    private readonly WebApplicationFactory<BusinessDayCounterController> _factory;

    public BusinessDayCounterControllerIntegrationTests(WebApplicationFactory<BusinessDayCounterController> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("2013-10-07", "2013-10-09", 1)]
    [InlineData("2013-10-05", "2013-10-14", 5)]
    [InlineData("2013-10-07", "2014-01-01", 61)]
    //[InlineData("2013-10-07", "2013-10-05", 0)]
    public async Task GetWeekdaysBetweenTwoDates_ReturnsCorrectResult(string firstDateString, string secondDateString, int expectedBusinessDays)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/BusinessDayCounter?firstDate={firstDateString}&secondDate={secondDateString}");
        response.EnsureSuccessStatusCode();       

        var content = await response.Content.ReadAsStringAsync();
        var businessDays = int.Parse(content);

        // Assert
        // You might need to assert the exact value based on your business logic and holiday rules.
        // For this example, let's assume a hypothetical value:
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
        var response = await client.GetAsync($"/BusinessDayCounter?firstDate={firstDateString}&secondDate={secondDateString}");        
        var content = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(content);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        problemDetails?.Detail.Should().Be("End date must be after start date");
    }
}