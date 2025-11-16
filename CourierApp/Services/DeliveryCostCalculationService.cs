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
            if (package == null)
                throw new ArgumentNullException(nameof(package));

            if (baseCost < 0)
                throw new ArgumentOutOfRangeException(nameof(baseCost), "Base cost cannot be negative.");

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
    }
}

