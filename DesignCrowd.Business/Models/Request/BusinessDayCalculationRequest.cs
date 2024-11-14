using System.ComponentModel.DataAnnotations;

namespace DesignCrowd.Business.Models.Request;

public class BusinessDayCalculationRequest
{
    [Required]
    public DateTime FirstDate { get; set; }

    [Required]
    public DateTime SecondDate { get; set; }

    [Required]
    public List<DateTime> Holidays { get; set; }
}