using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenFluxSmartChargingAPI.Dto
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CapacityInAmps { get; set; }
        public List<ChargeStation> ChargeStations { get; set; }
    }
}