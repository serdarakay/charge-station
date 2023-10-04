using Microsoft.AspNetCore.Mvc;
using GreenFluxSmartChargingAPI.Dto;


namespace GreenFluxSmartChargingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChargingController : ControllerBase
{
    // private readonly ChargingContext _context;

    // public ChargingController(ChargingContext context)
    // {
    //     _context = context;
    // }

    private static readonly string[] Summaries = new[]
{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<ChargingController> _logger;

    public ChargingController(ILogger<ChargingController> logger)
    {
        _logger = logger;
    }

    [HttpGet("test")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost("create-group")]
    public IActionResult CreateGroup([FromBody] Group group)
    {
        // Grup oluşturma kodunu buraya ekleyin
        // _context.Groups.Add(group);
        // _context.SaveChanges();

        return Ok(group);
    }

    [HttpPost("create-charge-station")]
    public IActionResult CreateChargeStation([FromBody] ChargeStation chargeStation)
    {
        // Şarj istasyonu oluşturma kodunu buraya ekleyin
        // _context.ChargeStations.Add(chargeStation);
        // _context.SaveChanges();

        return Ok(chargeStation);
    }

    // Diğer API yöntemlerini (güncelleme, silme vb.) ekleyin
}