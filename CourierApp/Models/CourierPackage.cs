
namespace CourierApp.Models
{
    public class CourierPackage
    {
        public string Id { get; set; } = string.Empty;
        public double Weight { get; set; }
        public double Dist { get; set; }
        public string OfferCode { get; set; } = string.Empty;
    }
}
