using CourierApp.Interfaces;
using CourierApp.Models;

namespace CourierApp.Services
{
    public class DeliveryCostCalculationService : ICostCalculation
    {
        private readonly IOfferService _offerService;

        public DeliveryCostCalculationService(IOfferService offerService)
        {
            _offerService = offerService;
        }

        public DeliveryOutput Calculate(CourierPackage package, double baseCost)
        {
            try
            {
                var deliveryCost = baseCost + (package.Weight * 10) + (package.Dist * 5);
                var discountPercentage = _offerService.GetDisPercent(package);
                var discount = deliveryCost * discountPercentage;
                var totalCost = deliveryCost - discount;

                return new DeliveryOutput
                {
                    PackId = package.Id,
                    Disc = Math.Round(discount, 2),
                    TotalCost = Math.Round(totalCost, 2),
                    Weight = package.Weight,
                    Distance = package.Dist
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while calculating cost for {package?.Id ?? "Unknown"}: {ex.Message}");
            }

            return new DeliveryOutput
            {
                PackId = package?.Id ?? "UNKNOWN",
                Disc = 0,
                TotalCost = 0,
                Weight = package?.Weight ?? 0,
                Distance = package?.Dist ?? 0
            };
        }
    }
}
