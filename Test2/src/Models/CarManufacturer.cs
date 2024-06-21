using System.ComponentModel.DataAnnotations;

namespace Test2.Models;

public class CarManufacturer
{
    public int Id { get; set; }
    
    [Required]
    [Length(1, 200)]
    public required string Name { get; set; }
}