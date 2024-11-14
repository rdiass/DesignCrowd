using DesignCrowd.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesignCrowd.Api.Controllers;

/// <summary>
/// Controller used to calculate weekdays between dates
/// </summary>
[ApiController]
[Route("[controller]")]
public class BusinessDayCounterController : ControllerBase
{
    private readonly ILogger<BusinessDayCounterController> _logger;
    private readonly IBusinessDayCounterService _businessDayCounterService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger">Logger to log informations, errors and warnings</param>
    /// <param name="businessDayCounter">Service injected by dependency injection</param>
    public BusinessDayCounterController(ILogger<BusinessDayCounterController> logger, IBusinessDayCounterService businessDayCounter)
    {
        _logger = logger;
        _businessDayCounterService = businessDayCounter;
    }

    /// <summary>
    /// Calculates the number of weekdays between two given dates.
    /// </summary>
    /// <param name="firstDate">The first date in the range.</param>
    /// <param name="secondDate">The second date in the range.</param>
    /// <returns>Ok message with the number of weekdays between the dates or an error message.</returns>
    /// <remarks>
    /// This endpoint returns the number of weekdays (excluding weekends) between the provided `firstDate` and `secondDate`.
    /// It validates the input dates to ensure `secondDate` is after `firstDate`. On successful validation, it calls the injected
    /// `IBusinessDayCounterService` to calculate the number of weekdays and returns an `Ok` response with the count.
    /// If the dates are invalid or an unexpected error occurs, it logs the error and returns a corresponding error response.
    /// </remarks>
    [HttpGet(Name = "WeekdaysBetweenTwoDates")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetWeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
    {
        try
        {
            // Validate if second date is after first date
            if (secondDate <= firstDate)
            {
                _logger.LogError("Invalid date range");
                return Problem("Second date must be after first date", statusCode: StatusCodes.Status400BadRequest);                
            }

            // Call service to calculate weekdays between the dates
            int businessDays = _businessDayCounterService.WeekdaysBetweenTwoDates(firstDate, secondDate);
            return Ok(businessDays);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred");
            return Problem("An unexpected error occurred", statusCode: StatusCodes.Status500InternalServerError);
        }     
    }
}

