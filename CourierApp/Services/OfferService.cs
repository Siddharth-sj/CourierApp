using CourierApp.Models;
using CourierApp.Interfaces;
using Microsoft.Extensions.Options;

namespace CourierApp.Services
{
    public class OfferService : IOfferService
    {
        private readonly List<OfferSpecs> _offers;

        public OfferService(IOptions<OfferSettings> offerSettings)
        {
            _offers = offerSettings.Value.Offers;
        }

        public double GetDisPercent(CourierPackage package)
        {
                if (package == null)
                    throw new ArgumentNullException(nameof(package), "Package cannot be null.");

                if (string.IsNullOrWhiteSpace(package.OfferCode))
                    return 0;

                if (_offers == null || !_offers.Any())
                    throw new InvalidOperationException("Offer configuration missing.");

                var offer = _offers.FirstOrDefault(o => o.CouponCode == package.OfferCode);

                if (offer == null) return 0;

                bool discountApplicable =
                    package.Dist >= offer.MinDist &&
                    package.Dist <= offer.MaxDist &&
                    package.Weight >= offer.MinWeight &&
                    package.Weight <= offer.MaxWeight;

                return discountApplicable ? offer.DiscountPercent : 0;
        }
    }
}
