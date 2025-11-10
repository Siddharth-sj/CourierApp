using CourierApp.Models;

namespace CourierApp.Interfaces
{
    public interface IOfferService
    {
        double GetDisPercent(CourierPackage package);
    }
}
