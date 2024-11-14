using DesignCrowd.Api.Swagger;
using DesignCrowd.Business.Interfaces;
using DesignCrowd.Business.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace DesignCrowd.Api.Controllers;

/// <summary>
/// Controller used to calculate weekdays between dates
/// </summary>
[ApiController]
[Route("api")]
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
    /// E.g. between Monday 07-Oct-2013 and Wednesday 09-Oct-2013 is one weekday
    /// </remarks>
    [HttpGet("weekdays")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetWeekdaysBetweenTwoDates(
        [SwaggerTryItOutDefaulValue("2013-10-07")] DateTime firstDate,
        [SwaggerTryItOutDefaulValue("2013-10-09")] DateTime secondDate)
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

    /// <summary>
    /// Calculates the number of business days between two given dates, considering specified holidays.
    /// </summary>
    /// <param name="request">A request object containing the start date, end date, and a list of holidays.</param>
    /// <returns>
    /// An `Ok` response with the number of business days if the request is valid and the calculation is successful.
    /// A `BadRequest` response if the second date is before the first date.
    /// A `InternalServerError` response for unexpected errors.
    /// </returns>
    /// <remarks>
    /// This endpoint returns the number of weekdays (excluding weekends and given `holidays`) between the provided `firstDate` and `secondDate`.        
    /// #### Sample request:
    /// ```json
    ///{
    ///  "firstDate": "2013-10-07",
    ///  "secondDate": "2014-01-01",
    ///  "holidays": [
    ///    "2013-12-25", "2013-12-26", "2014-01-01"
    ///  ]
    ///}
    /// ```
    /// #### Expected result: `59`
    /// </remarks>
    [HttpPost("businessdays")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public IActionResult GetBusinessDaysBetweenTwoDatesWithHolidays(
            [FromBody] BusinessDayCalculationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Validate if second date is after first date
            if (request.SecondDate <= request.FirstDate)
            {
                _logger.LogError("Invalid date range");
                return Problem("Second date must be after first date", statusCode: StatusCodes.Status400BadRequest);
            }

            // Calculate business days with specified holidays
            int businessDays = _businessDayCounterService.BusinessDaysBetweenTwoDates(
                request.FirstDate,
                request.SecondDate,
                request.Holidays);

            return Ok(businessDays);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred");
            return Problem("An unexpected error occurred", statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}

