using System.Text.Json.Serialization;

namespace GreenFluxSmartChargingAPI.Dto
{
    public class Group : Models.Group
    {
        [JsonIgnore]
        public List<ChargeStation>? ChargeStations { get; set; }
    }
}