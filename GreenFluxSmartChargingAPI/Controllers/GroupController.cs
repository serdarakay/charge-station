using Microsoft.AspNetCore.Mvc;
using GreenFluxSmartChargingAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace GreenFluxSmartChargingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{

    public readonly DataContext _context;

    public GroupController(DataContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult GetAll()
    {

        try
        {
            var groups = _context.Groups.Include(x => x.ChargeStations).ToList();

            return Ok(groups);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);

        }
    }
    [HttpPost]
    public IActionResult CreateGroup([FromBody] Models.Group group)
    {

        try
        {
            if (group == null)
            {
                throw new Exception("Model can not be Empty...");
            }
            Dto.Group data = new()
            {
                Name = group.Name,
                CapacityInAmps = group.CapacityInAmps
            };

            _context.Groups.Add(data);
            _context.SaveChanges();

            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("{id}")]
    public IActionResult UpdateGroup(int id, [FromBody] Models.Group group)
    {

        try
        {
            Dto.Group existingGroup = _context.Groups.FirstOrDefault(x => x.Id == id);

            if (existingGroup == null)
            {
                throw new Exception("No Group Found");
            }

            existingGroup.Name = group.Name;
            existingGroup.CapacityInAmps = group.CapacityInAmps;

            _context.Attach(existingGroup);
            _context.Entry(existingGroup).State = EntityState.Modified;

            _context.SaveChanges();

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(group);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteGroup(int id)
    {

        try
        {
            var existingGroup = _context.Groups.FirstOrDefault(x => x.Id == id);

            if (existingGroup == null)
            {
                throw new Exception("No Group Found");
            }

            var chargeStationsToDelete = _context.ChargeStations.Where(x => x.GroupId == existingGroup.Id).ToList();

            _context.RemoveRange(chargeStationsToDelete);

            _context.Remove(existingGroup);

            _context.SaveChanges();

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }
}