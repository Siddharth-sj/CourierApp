using CourierApp.Interfaces;
using CourierApp.Models;

namespace CourierApp.Services
{
    public class DeliveryTimeCalculationService : IDeliveryTime
    {
        public List<DeliveryOutput> TimeCalculation(List<DeliveryOutput> packages, VehSettings vehicles)
        {
            var results = new List<DeliveryOutput>();
            try
            {
                var vehTimes = new double[vehicles.NumOfVehicles];

                var pkdData = packages.OrderByDescending(p => p.Weight).ToList();

                while (pkdData.Any())
                {
                    int nextVehicle = Array.IndexOf(vehTimes, vehTimes.Min());
                    double currentTime = vehTimes[nextVehicle];

                    var shipment = SelectPackagesForTrip(pkdData, vehicles.MaxWeight);

                    double maxDistance = shipment.Max(p => p.Distance);
                    double deliveryTime = maxDistance / vehicles.MaxSpeed;

                    foreach (var pkg in shipment)
                    {
                        pkg.EstDelTime = Math.Round(currentTime + (pkg.Distance / vehicles.MaxSpeed), 2);
                        results.Add(pkg);
                    }

                    vehTimes[nextVehicle] += deliveryTime * 2;

                    pkdData.RemoveAll(p => shipment.Contains(p));
                }

                return results.OrderBy(p => p.EstDelTime).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in delivery time calculation: {ex.Message}");
            }
            return results;
        }

        private List<DeliveryOutput> SelectPackagesForTrip(List<DeliveryOutput> packages, double maxCapacity)
        {
            var selected = new List<DeliveryOutput>();
            double totalWeight = 0;

            try
            {
                foreach (var pkg in packages.OrderByDescending(p => p.Weight))
                {
                    if (totalWeight + pkg.Weight <= maxCapacity)
                    {
                        selected.Add(pkg);
                        totalWeight = totalWeight + pkg.Weight;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while selecting packages for trip: {ex.Message}");
            }

            return selected;
        }
    }
}
