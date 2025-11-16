
namespace CourierApp.Models
{
    public class OfferSpecs
    {
        public string? CouponCode { get; set; }
        public double DiscountPercent { get; set; }
        public double MinDist { get; set; }
        public double MaxDist { get; set; }
        public double MinWeight { get; set; }
        public double MaxWeight { get; set; }
    }
}
