using DesignCrowd.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesignCrowd.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BusinessDayCounterController : ControllerBase
{
    private readonly ILogger<BusinessDayCounterController> _logger;
    private readonly IBusinessDayCounterService _businessDayCounterService;
    public BusinessDayCounterController(ILogger<BusinessDayCounterController> logger, IBusinessDayCounterService businessDayCounter)
    {
        _logger = logger;
        _businessDayCounterService = businessDayCounter;
    }

    [HttpGet(Name = "WeekdaysBetweenTwoDates")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetWeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
    {
        try
        {
            if (secondDate <= firstDate)
            {
                _logger.LogError("Invalid date range");
                return Problem("Second date must be after first date", statusCode: StatusCodes.Status400BadRequest);                
            }

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

