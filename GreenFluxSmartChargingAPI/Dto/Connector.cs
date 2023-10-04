using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenFluxSmartChargingAPI.Dto
{
    public class Connector
    {
        public int Id { get; set; }
        public int ChargeStationId { get; set; }
        public int MaxCurrentInAmps { get; set; }
    }
}