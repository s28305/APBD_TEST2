using System.ComponentModel.DataAnnotations;

namespace Test2.DTO;

public class DriverCompetitionDto
{
    [Required]
    public int DriverId { get; set; }

    [Required]
    public int CompetitionId { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
}