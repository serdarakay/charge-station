using Microsoft.AspNetCore.Mvc;
using GreenFluxSmartChargingAPI.Data;

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
    public IActionResult GetAll([FromBody] Models.Connector connector)
    {

        return Ok(connector);
    }
    [HttpPost]
    public IActionResult CreateConnector([FromBody] Models.Connector connector)
    {

        return Ok(connector);
    }
    [HttpPut]
    public IActionResult UpdateConnector(int id, [FromBody] Models.Connector connector)
    {

        return Ok(connector);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteConnector(int id)
    {

        return Ok(true);
    }
}