using CourierApp.Models;

namespace CourierApp.Interfaces
{
    public interface ICostCalculation
    {
        DeliveryOutput Calculate(CourierPackage package, double baseCost);
    }
}
