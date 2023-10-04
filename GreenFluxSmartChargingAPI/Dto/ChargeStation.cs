using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenFluxSmartChargingAPI.Dto
{
    public class ChargeStation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public List<Connector> Connectors { get; set; }
    }
}