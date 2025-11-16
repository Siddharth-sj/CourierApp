using CourierApp.Models;
using CourierApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp.Tests.Services
{
    public class DeliveryTimeCalculationServiceTests
    {
        [Fact]
        public void ShouldMatchOfficialSampleOutput()
        {
            var service = new DeliveryTimeCalculationService();

            var packages = new List<DeliveryOutput>
            {
                new DeliveryOutput { PackId="PKG1", Weight=50, Distance=30 },
                new DeliveryOutput { PackId="PKG2", Weight=75, Distance=125 },
                new DeliveryOutput { PackId="PKG3", Weight=175, Distance=100 },
                new DeliveryOutput { PackId="PKG4", Weight=110, Distance=60 },
                new DeliveryOutput { PackId="PKG5", Weight=155, Distance=95 }
            };

            var vehicles = new VehSettings
            {
                NumOfVehicles = 2,
                MaxSpeed = 70,
                MaxWeight = 200
            };

            var result = service.TimeCalculation(packages, vehicles);

            Assert.Equal(1.78, result.Single(x => x.PackId == "PKG2").EstDelTime);
            Assert.Equal(0.85, result.Single(x => x.PackId == "PKG4").EstDelTime);
            Assert.Equal(1.42, result.Single(x => x.PackId == "PKG3").EstDelTime);
            Assert.Equal(4.19, result.Single(x => x.PackId == "PKG5").EstDelTime);
            Assert.Equal(3.98, result.Single(x => x.PackId == "PKG1").EstDelTime);
        }
    }
}
