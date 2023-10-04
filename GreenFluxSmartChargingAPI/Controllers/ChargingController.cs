using Microsoft.AspNetCore.Mvc;
using GreenFluxSmartChargingAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace GreenFluxSmartChargingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChargingController : ControllerBase
{

    public readonly DataContext _context;

    public ChargingController(DataContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var data = _context.ChargeStations.Include(x => x.Connectors).ToList();

            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);

        }
    }
    [HttpPost]
    public IActionResult CreateCharging([FromBody] Models.ChargeStation chargeStation)
    {
        try
        {
            if (chargeStation == null)
            {
                throw new Exception("Model can not be Empty");
            }

            Dto.ChargeStation data = new()
            {
                Name = chargeStation.Name,
                GroupId = chargeStation.GroupId
            };

            _context.ChargeStations.Add(data);

            _context.SaveChanges();

            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); 
        }
    }
    [HttpPut("{id}")]
    public IActionResult UpdateCharging(int id, [FromBody] Models.ChargeStation chargeStation)
    {
        try
        {
            Dto.ChargeStation existingChargeStation = _context.ChargeStations.FirstOrDefault(x => x.Id == id);

            if (existingChargeStation == null)
            {
                throw new Exception("No Group Found");
            }
        }
        catch (Exception ex)
        {

        }
        return Ok(chargeStation);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCharging(int id)
    {

        return Ok(true);
    }
}