using CourierApp.Models;
using CourierApp.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp.Tests.Services
{
    public class OfferServiceTests
    {
        [Fact]
        public void Discount_ShouldApply_WhenRulesMatch()
        {
            var settings = Options.Create(new OfferSettings
            {
                Offers = new List<OfferSpecs>
                {
                    new OfferSpecs {
                        CouponCode = "OFR001",
                        DiscountPercent = 0.1,
                        MinDist = 10, MaxDist = 100,
                        MinWeight = 1, MaxWeight = 100
                    }
                }
            });

            var service = new OfferService(settings);

            var pkg = new CourierPackage
            {
                OfferCode = "OFR001",
                Dist = 50,
                Weight = 50
            };

            Assert.Equal(0.1, service.GetDisPercent(pkg));
        }
    }
}
