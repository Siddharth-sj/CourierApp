using CourierApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp.Tests.Models
{
    public class ModelValidationTests
    {
        [Fact]
        public void CourierPackage_ShouldSetProperties()
        {
            var pkg = new CourierPackage
            {
                Id = "PKG1",
                Weight = 20,
                Dist = 100,
                OfferCode = "OFR001"
            };

            Assert.Equal("PKG1", pkg.Id);
            Assert.Equal(20, pkg.Weight);
            Assert.Equal(100, pkg.Dist);
            Assert.Equal("OFR001", pkg.OfferCode);
        }
    }
}
