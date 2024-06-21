using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test2.DTO;
using Test2.Helpers;
using Test2.Models;

namespace Test2.Controllers;

[Route("api/drivers")]
[ApiController]
public class DriverController: ControllerBase
{
    private readonly DriverContext _context;

    public DriverController(DriverContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DriverDto>>> GetDrivers()
    {
        var drivers = await _context.Drivers.Include(c => c.Car)
            .OrderBy(c => c.FirstName)
            .ToListAsync();

        return drivers.Select(MapToDriverDto).ToList();
    }
        
    [HttpGet("{id}")]
    public async Task<ActionResult<GetDriverDto>> GetDriver(int id)
    {
        var driver = await _context.Drivers.FindAsync(id);

        if (driver == null)
        {
            return NotFound();
        }

        return MapToGetDriverDto(driver);
    }
    
    [HttpGet("/competitions")]
    public async Task<ActionResult<IEnumerable<Competition>>> GetCompetitions()
    {
        var comps = await _context.Competitions.ToListAsync();

        return comps.ToList();
    }
    
    [HttpPost]
    public async Task<ActionResult<DriverDto>> PostDriver([FromBody] DriverDto driver)
    {
        try
        {
            var car = await _context.Cars.FindAsync(driver.CarId);
            if (car == null)
            {
                return BadRequest("Car with given Id does not exist");
            }

            var newDriver = new Driver
            {
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                Birthday = driver.Birthday,
                CarId = driver.CarId
            };
            
            _context.Drivers.Add(newDriver);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriver", new { id = newDriver.Id }, MapToDriverDto(newDriver));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = "Error occurred while adding visit.", details = e.Message });
        }
    }
    
    [HttpPost("/competitions")]
    public async Task<ActionResult<DriverDto>> AssignToCompetition(int competitionId, int driverId)
    {
        try
        {
            var driver = await _context.Drivers.FindAsync(driverId);
            if (driver == null)
            {
                return BadRequest("Driver with given Id does not exist");
            }
            
            var comp = await _context.Competitions.FindAsync(competitionId);
            if (comp == null)
            {
                return BadRequest("Competition with given Id does not exist");
            }

            var newDriverComp = new DriverCompetition
            {
                DriverId = driverId,
                CompetitionId = competitionId,
                Date = DateTime.Now
            };
            
            _context.DriversCompetitions.Add(newDriverComp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("", new { id = newDriverComp.DriverId, newDriverComp.CompetitionId }, MapToDriverCompDto(newDriverComp));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = "Error occurred while adding visit.", details = e.Message });
        }
    }

    private DriverCompetitionDto MapToDriverCompDto(DriverCompetition driverCompetition)
    {
        return new DriverCompetitionDto
        {
            DriverId = driverCompetition.DriverId,
            CompetitionId = driverCompetition.CompetitionId,
            Date = driverCompetition.Date
        };
    }


    private static GetDriverDto MapToGetDriverDto(Driver driver)
    { 
        return new GetDriverDto
        {
            CarNumber = driver.Car.Number,
            ManufacturerName = driver.Car.CarManufacturer.Name,
            Model = driver.Car.ModelName 
        };
    }
    
    private static DriverDto MapToDriverDto(Driver driver)
    { 
        return new DriverDto
        {
            FirstName = driver.FirstName,
            LastName = driver.LastName,
            Birthday = driver.Birthday,
            CarId = driver.CarId
        };
    }
    
}