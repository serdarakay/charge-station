using GreenFluxSmartChargingAPI.Controllers;
using GreenFluxSmartChargingAPI.Data;
using GreenFluxSmartChargingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace XUnitTest;

public class ChargingTest
{

    public readonly DataContext _context;

    public readonly DbContextOptions<DataContext> options;

    public ChargingTest()
    {
        options = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
           .Options;

        _context = new DataContext(options);
    }

    [Fact]
    public void Get_ChargeStations()
    {

        using (var context = new DataContext(options))
        {
            // Arrange

            ChargingController controller = new ChargingController(context);

            context.Groups.Add(new GreenFluxSmartChargingAPI.Dto.Group { Name = "test1", CapacityInAmps = 10, Id = 1 });
            context.ChargeStations.Add(new GreenFluxSmartChargingAPI.Dto.ChargeStation { Name = "charge-station-1", GroupId = 1, Id = 1 });
            context.SaveChanges();

            // Act

            var result = controller.GetAll() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            List<GreenFluxSmartChargingAPI.Dto.ChargeStation> chargeStations = Assert.IsType<List<GreenFluxSmartChargingAPI.Dto.ChargeStation>>(result.Value);
            Assert.NotEmpty(chargeStations);
        }


    }

    [Fact]
    public void Create_ChargeStation()
    {
        using (var context = new DataContext(options))
        {
            // Arrange
            ChargingController controller = new ChargingController(context);
            context.Groups.Add(new GreenFluxSmartChargingAPI.Dto.Group { Name = "test1", CapacityInAmps = 10, Id = 2 });
            context.SaveChanges();
            var newChargeStation = new ChargeStation
            {
                Name = "New Charge Station",
                GroupId = 2,
                Id = 2
            };

            // Act
            var result = controller.CreateChargeStation(newChargeStation) as OkObjectResult;
            var resultValue = JsonConvert.DeserializeObject<ChargeStation>(JsonConvert.SerializeObject(result.Value));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newChargeStation.Name, resultValue.Name);
            Assert.Equal(200, result.StatusCode);

            var createdChargeStation = context.ChargeStations.FirstOrDefault(g => g.Name == "New Charge Station");
            Assert.NotNull(createdChargeStation);
            Assert.Equal(newChargeStation.Name, createdChargeStation.Name);
        }

    }

    [Fact]
    public void Update_ChargeStation()
    {

        using (var context = new DataContext(options))
        {
            // Arrange
            ChargingController controller = new ChargingController(context);
            _context.Groups.Add(new GreenFluxSmartChargingAPI.Dto.Group { Name = "New Group", CapacityInAmps = 10, Id = 3 });
            _context.ChargeStations.Add(new GreenFluxSmartChargingAPI.Dto.ChargeStation { Name = "New Charge Station", Id = 3 });
            _context.SaveChanges();

            var chargeStationToUpdate = new ChargeStation
            {
                Id = 3,
                Name = "Updated Group"
            };

            // Act
            var result = controller.UpdateChargeStation(3, chargeStationToUpdate) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var updatedChargeStation = context.ChargeStations.FirstOrDefault(g => g.Id == chargeStationToUpdate.Id);
            Assert.NotNull(updatedChargeStation);
            Assert.Equal(chargeStationToUpdate.Name, updatedChargeStation.Name);
        }


    }

    [Fact]
    public void Delete_ChargeStation()
    {

        using (var context = new DataContext(options))
        {
            // Arrange
            ChargingController controller = new ChargingController(context);
            _context.Groups.Add(new GreenFluxSmartChargingAPI.Dto.Group { Name = "New Group", CapacityInAmps = 10, Id = 4 });
            _context.ChargeStations.Add(new GreenFluxSmartChargingAPI.Dto.ChargeStation { Name = "New Charge Station", Id = 4 });
            _context.SaveChanges();
            int chargeStationIdToDelete = 4;

            // Act
            var result = controller.DeleteChargeStation(chargeStationIdToDelete) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var deletedChargeStation = context.ChargeStations.FirstOrDefault(g => g.Id == chargeStationIdToDelete);
            Assert.Null(deletedChargeStation);
        }

    }
}
