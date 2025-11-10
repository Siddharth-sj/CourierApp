
namespace CourierApp.Models
{
    public class DeliveryOutput
    {
        public string PackId { get; set; } = string.Empty;
        public double Weight { get; set; }
        public double Distance { get; set; }
        public double Disc { get; set; }
        public double TotalCost { get; set; }
        public double EstDelTime { get; set; }
    
    }
}
