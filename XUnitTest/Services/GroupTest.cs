using GreenFluxSmartChargingAPI.Controllers;
using GreenFluxSmartChargingAPI.Data;
using GreenFluxSmartChargingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace XUnitTest;

public class GroupTest
{

    public readonly DataContext _context;

    public readonly DbContextOptions<DataContext> options;

    public GroupTest()
    {
        options = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
           .Options;

        _context = new DataContext(options);
    }

    [Fact]
    public void Get_Groups()
    {

        using (var context = new DataContext(options))
        {
            // Arrange

            GroupController controller = new GroupController(context);

            context.Groups.Add(new GreenFluxSmartChargingAPI.Dto.Group { Name = "test1", CapacityInAmps = 10, Id = 1 });
            context.SaveChanges();

            // Act

            var result = controller.GetAll() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            List<GreenFluxSmartChargingAPI.Dto.Group> chargeStations = Assert.IsType<List<GreenFluxSmartChargingAPI.Dto.Group>>(result.Value);
            Assert.NotEmpty(chargeStations);
        }


    }

    [Fact]
    public void Create_Group()
    {
        using (var context = new DataContext(options))
        {
            // Arrange
            GroupController controller = new GroupController(context);
            var newGroup = new Group
            {
                Name = "New Group",
                CapacityInAmps = 20,
                Id = 2
            };

            // Act
            var result = controller.CreateGroup(newGroup) as OkObjectResult;

            var resultValue = JsonConvert.DeserializeObject<Group>(JsonConvert.SerializeObject(result.Value));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newGroup.Name, resultValue.Name);
            Assert.Equal(newGroup.CapacityInAmps, resultValue.CapacityInAmps);
            Assert.Equal(200, result.StatusCode);

            var createdGroup = context.Groups.FirstOrDefault(g => g.Name == "New Group");
            Assert.NotNull(createdGroup);
            Assert.Equal("New Group", createdGroup.Name);
            Assert.Equal(20, createdGroup.CapacityInAmps);
        }

    }

    [Fact]
    public void Update_Group()
    {

        using (var context = new DataContext(options))
        {
            // Arrange
            GroupController controller = new GroupController(context);
            _context.Groups.Add(new GreenFluxSmartChargingAPI.Dto.Group { Name = "New Group", CapacityInAmps = 10, Id = 3 });
            _context.SaveChanges();

            var groupToUpdate = new Group
            {
                Id = 3,
                Name = "Updated Group",
                CapacityInAmps = 30
            };

            // Act
            var result = controller.UpdateGroup(3, groupToUpdate) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var updatedGroup = context.Groups.FirstOrDefault(g => g.Id == groupToUpdate.Id);
            Assert.NotNull(updatedGroup);
            Assert.Equal(groupToUpdate.Name, updatedGroup.Name);
            Assert.Equal(30, updatedGroup.CapacityInAmps);
        }


    }

    [Fact]
    public void Delete_Group()
    {

        using (var context = new DataContext(options))
        {
            // Arrange
            GroupController controller = new GroupController(context);
            _context.Groups.Add(new GreenFluxSmartChargingAPI.Dto.Group { Name = "New Group", CapacityInAmps = 10, Id = 4 });
            _context.SaveChanges();
            int groupIdToDelete = 4;

            // Act
            var result = controller.DeleteGroup(groupIdToDelete) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var deletedGroup = context.ChargeStations.FirstOrDefault(g => g.Id == groupIdToDelete);
            Assert.Null(deletedGroup);
        }

    }
}
