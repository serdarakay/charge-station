namespace GreenFluxSmartChargingAPI.Models
{
    public class Connector
    {
        public int Id { get; set; }
        public int ChargeStationId { get; set; }
        public int MaxCurrentInAmps { get; set; }
    }
}