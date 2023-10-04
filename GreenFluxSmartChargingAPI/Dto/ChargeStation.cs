using System.Text.Json.Serialization;

namespace GreenFluxSmartChargingAPI.Dto
{
    public class ChargeStation : Models.ChargeStation
    {
        [JsonIgnore]
        public List<Connector> Connectors { get; set; }

        [JsonIgnore]
        public Group Group { get; set; }
    }
}