using CourierApp.Models;

namespace CourierApp.Interfaces
{
    public interface IDeliveryTime
    {
        List<DeliveryOutput> TimeCalculation(List<DeliveryOutput> packages, VehSettings vehicles);
    }
}
