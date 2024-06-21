using System.ComponentModel.DataAnnotations;

namespace Test2.Models;

public class Car
{
    public int Id { get; set; }
    
    [Required]
    public int CarManufacturerId { get; set; }
    
    [Required]
    [Length(1, 200)]
    public required string ModelName { get; set; }
    
    [Required]
    public int Number { get; set; }
    
    
    public CarManufacturer CarManufacturer { get; set; }
    
    [Timestamp]
    public byte[] ConcurrencyToken { get; set; }
}