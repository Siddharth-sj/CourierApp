using CourierApp.Interfaces;
using CourierApp.Models;
using CourierApp.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp.Tests.Services
{
    public class DeliveryCostCalculationServiceTests
    {
        [Fact]
        public void Calculate_ShouldReturnCorrectCost()
        {
            var offerMock = new Mock<IOfferService>();
            offerMock.Setup(o => o.GetDisPercent(It.IsAny<CourierPackage>()))
                .Returns(0.10);

            var service = new DeliveryCostCalculationService(offerMock.Object);

            var pkg = new CourierPackage { Id = "PKG1", Weight = 50, Dist = 30 };

            var result = service.Calculate(pkg, 100);

            Assert.Equal("PKG1", result.PackId);
            Assert.Equal(75, result.Disc);       // (100 + 500 + 150) * 10%
            Assert.Equal(675, result.TotalCost); // 750 - 75
        }

        [Fact]
        public void Calculate_ShouldThrow_WhenPackageNull()
        {
            var offerMock = new Mock<IOfferService>();
            var service = new DeliveryCostCalculationService(offerMock.Object);

            Assert.Throws<ArgumentNullException>(() => service.Calculate(null!, 100));
        }
    }
}
