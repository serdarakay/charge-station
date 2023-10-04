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

            var selectedGroup = _context.ChargeStations.Where(x => x.Id == connector.ChargeStationId).Select(x => x.Group).FirstOrDefault();

            if (selectedGroup == null)
            {
                throw new Exception("There is no Group to calculate max amps value");
            }
            if (!IsGroupCapacitySufficient(selectedGroup))
            {
                throw new Exception("The sum of MaxCurrentInAmps values exceeds the Group capacity.");
            }
            if (connector.MaxCurrentInAmps <= 0)
            {
                throw new Exception("MaxCurrentInAmps value has to be greater than 0");
            }

            Dto.Connector data = new()
            {
                ChargeStationId = connector.ChargeStationId,
                MaxCurrentInAmps = connector.MaxCurrentInAmps,
                Identifier = connector.Identifier
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
            if (connector.MaxCurrentInAmps <= 0)
            {
                throw new Exception("MaxCurrentInAmps value has to be greater than 0");
            }

            existingConnector.ChargeStationId = connector.ChargeStationId;
            existingConnector.MaxCurrentInAmps = connector.MaxCurrentInAmps;
            existingConnector.Identifier = connector.Identifier;

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

    public bool IsGroupCapacitySufficient(Dto.Group group)
    {
        if (group == null)
        {
            throw new ArgumentNullException(nameof(group));
        }

        int totalMaxCurrent = group.ChargeStations?.SelectMany(cs => cs.Connectors).Sum(c => c.MaxCurrentInAmps) ?? 0;

        return group.CapacityInAmps >= totalMaxCurrent;
    }
}