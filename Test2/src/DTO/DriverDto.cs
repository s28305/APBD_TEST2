using System.ComponentModel.DataAnnotations;
using Test2.Models;

namespace Test2.DTO;

public class DriverDto
{
    [Required]
    [Length(1, 200)]
    public required string FirstName { get; set; }
    
    [Required]
    [Length(1, 200)]
    public required string LastName { get; set; }
    
    [Required]
    public required DateTime Birthday { get; set; }
    
    [Required]
    public int CarId { get; set; }

    public DriverDto()
    {
        
    }

    public DriverDto(Driver driver)
    {
        FirstName = driver.FirstName;
        LastName = driver.LastName;
        Birthday = driver.Birthday;
        CarId = driver.CarId;
    }
}