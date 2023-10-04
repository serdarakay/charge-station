using System.ComponentModel.DataAnnotations;
namespace GreenFluxSmartChargingAPI.Models
{
    public class Connector
    {
        public int Id { get; set; }
        public int ChargeStationId { get; set; }
        public int MaxCurrentInAmps { get; set; }
        [Range(1, 5, ErrorMessage = "Identifier must be between 1 and 5.")]
        public int Identifier { get; set; }

    }
}