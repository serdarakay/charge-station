using Microsoft.AspNetCore.Mvc;
using GreenFluxSmartChargingAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace GreenFluxSmartChargingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConnectorController : ControllerBase
{

    public readonly DataContext _context;

    public ConnectorController(DataContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var data = _context.Connectors.ToList();

            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    public IActionResult CreateConnector([FromBody] Models.Connector connector)
    {
        try
        {
            if (connector == null)
            {
                throw new Exception("Model can not be Empty");
            }

            Dto.Connector data = new()
            {
                ChargeStationId = connector.ChargeStationId,
                MaxCurrentInAmps = connector.MaxCurrentInAmps
            };

            _context.Connectors.Add(data);

            _context.SaveChanges();

            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("{id}")]
    public IActionResult UpdateConnector(int id, [FromBody] Models.Connector connector)
    {

        try
        {
            if (connector == null)
            {
                throw new Exception("Model can not be Empty");
            }

            var existingConnector = _context.Connectors.FirstOrDefault(x => x.Id == id);

            if (existingConnector == null)
            {
                throw new Exception("There is no data to Update");
            }

            existingConnector.ChargeStationId = connector.ChargeStationId;
            existingConnector.MaxCurrentInAmps = connector.MaxCurrentInAmps;

            _context.Attach(existingConnector);
            _context.Entry(existingConnector).State = EntityState.Modified;

            _context.SaveChanges();

            return Ok(existingConnector);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteConnector(int id)
    {
        try
        {
            var existingConnector = _context.Connectors.FirstOrDefault(x => x.Id == id);

            if (existingConnector == null)
            {
                throw new Exception("No Group Found");
            }

            _context.Remove(existingConnector);

            _context.SaveChanges();

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}