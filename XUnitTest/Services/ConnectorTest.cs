using GreenFluxSmartChargingAPI.Controllers;
using GreenFluxSmartChargingAPI.Data;
using GreenFluxSmartChargingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace XUnitTest;

public class ConnectorTest
{

    public readonly DataContext _context;

    public readonly DbContextOptions<DataContext> options;

    public ConnectorTest()
    {
        options = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
           .Options;

        _context = new DataContext(options);
    }

    [Fact]
    public void Get_Connectors()
    {

        using (var context = new DataContext(options))
        {
            // Arrange

            ConnectorController controller = new ConnectorController(context);

            context.Groups.Add(new GreenFluxSmartChargingAPI.Dto.Group { Name = "test1", CapacityInAmps = 10, Id = 1 });
            context.ChargeStations.Add(new GreenFluxSmartChargingAPI.Dto.ChargeStation { Name = "charge-station-1", GroupId = 1, Id = 1 });
            context.Connectors.Add(new GreenFluxSmartChargingAPI.Dto.Connector { MaxCurrentInAmps = 1, ChargeStationId = 1, Id = 1 });
            context.SaveChanges();

            // Act

            var result = controller.GetAll() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            List<GreenFluxSmartChargingAPI.Dto.Connector> connectors = Assert.IsType<List<GreenFluxSmartChargingAPI.Dto.Connector>>(result.Value);
            Assert.NotEmpty(connectors);
        }


    }

    [Fact]
    public void Create_Connector()
    {
        using (var context = new DataContext(options))
        {
            // Arrange
            ConnectorController controller = new ConnectorController(context);
            context.Groups.Add(new GreenFluxSmartChargingAPI.Dto.Group { Name = "test1", CapacityInAmps = 10, Id = 2 });
            context.ChargeStations.Add(new GreenFluxSmartChargingAPI.Dto.ChargeStation { Name = "test1", GroupId = 2, Id = 2 });
            context.SaveChanges();
            var newConnector = new Connector
            {
                ChargeStationId = 2,
                MaxCurrentInAmps = 2,
                Id = 2
            };

            // Act
            var result = controller.CreateConnector(newConnector) as OkObjectResult;
            var resultValue = JsonConvert.DeserializeObject<Connector>(JsonConvert.SerializeObject(result.Value));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newConnector.MaxCurrentInAmps, resultValue.MaxCurrentInAmps);
            Assert.Equal(200, result.StatusCode);

            var createdConnector = context.Connectors.FirstOrDefault(g => g.Id == newConnector.Id);
            Assert.NotNull(createdConnector);
            Assert.Equal(newConnector.MaxCurrentInAmps, createdConnector.MaxCurrentInAmps);
        }

    }

    [Fact]
    public void Update_Connector()
    {

        using (var context = new DataContext(options))
        {
            // Arrange
            ConnectorController controller = new ConnectorController(context);
            _context.Groups.Add(new GreenFluxSmartChargingAPI.Dto.Group { Name = "New Group", CapacityInAmps = 10, Id = 3 });
            _context.ChargeStations.Add(new GreenFluxSmartChargingAPI.Dto.ChargeStation { Name = "New Charge Station", Id = 3 });
            _context.Connectors.Add(new GreenFluxSmartChargingAPI.Dto.Connector { MaxCurrentInAmps = 3, ChargeStationId = 3, Id = 3 });
            _context.SaveChanges();

            var connectorToUpdate = new Connector
            {
                Id = 3,
                MaxCurrentInAmps = 35
            };

            // Act
            var result = controller.UpdateConnector(3, connectorToUpdate) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var updatedConnector = context.Connectors.FirstOrDefault(g => g.Id == connectorToUpdate.Id);
            Assert.NotNull(updatedConnector);
            Assert.Equal(updatedConnector.MaxCurrentInAmps, updatedConnector.MaxCurrentInAmps);
        }


    }

    [Fact]
    public void Delete_Connector()
    {

        using (var context = new DataContext(options))
        {
            // Arrange
            ConnectorController controller = new ConnectorController(context);
            _context.Groups.Add(new GreenFluxSmartChargingAPI.Dto.Group { Name = "New Group", CapacityInAmps = 10, Id = 4 });
            _context.ChargeStations.Add(new GreenFluxSmartChargingAPI.Dto.ChargeStation { Name = "New Charge Station", Id = 4 });
            _context.Connectors.Add(new GreenFluxSmartChargingAPI.Dto.Connector { MaxCurrentInAmps = 4, ChargeStationId = 4, Id = 4 });
            _context.SaveChanges();
            int connectorIdToDelete = 4;

            // Act
            var result = controller.DeleteConnector(connectorIdToDelete) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var deletedConnector = context.Connectors.FirstOrDefault(g => g.Id == connectorIdToDelete);
            Assert.Null(deletedConnector);
        }

    }
}
