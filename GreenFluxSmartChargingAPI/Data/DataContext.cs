using Microsoft.EntityFrameworkCore;
using GreenFluxSmartChargingAPI.Dto;

namespace GreenFluxSmartChargingAPI.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base (options)
		{
			
		}

		public DbSet<Group> Groups => Set<Group>();
		public DbSet<ChargeStation> ChargeStations => Set<ChargeStation>();
		public DbSet<Connector> Connectors => Set<Connector>();
    }
}

