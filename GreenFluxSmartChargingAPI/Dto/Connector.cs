using System.Text.Json.Serialization;

namespace GreenFluxSmartChargingAPI.Dto
{
    public class Connector : Models.Connector
    {

        [JsonIgnore]
        public ChargeStation ChargeStation { get; set; }
    }
}