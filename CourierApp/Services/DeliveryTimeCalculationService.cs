using CourierApp.Interfaces;
using CourierApp.Models;

namespace CourierApp.Services
{
    public class DeliveryTimeCalculationService : IDeliveryTime
    {
        public List<DeliveryOutput> TimeCalculation(List<DeliveryOutput> packages, VehSettings vehicles)
        {
            if (vehicles.NumOfVehicles <= 0)
                throw new ArgumentOutOfRangeException(nameof(vehicles.NumOfVehicles));

            if (vehicles.MaxSpeed <= 0)
                throw new ArgumentOutOfRangeException(nameof(vehicles.MaxSpeed));

            if (vehicles.MaxWeight <= 0)
                throw new ArgumentOutOfRangeException(nameof(vehicles.MaxWeight));

            if (packages == null || !packages.Any())
                throw new ArgumentException("No packages supplied for delivery");

            var results = new List<DeliveryOutput>();
            var vehTimes = new double[vehicles.NumOfVehicles];

            var pkdData = packages.OrderByDescending(p => p.Weight).ToList();

            while (pkdData.Any())
            {
                int nextVehicle = Array.IndexOf(vehTimes, vehTimes.Min());
                double currentTime = vehTimes[nextVehicle];

                var shipment = SelectPackagesForTrip(pkdData, vehicles.MaxWeight, vehicles.MaxSpeed);

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

        private List<DeliveryOutput> SelectPackagesForTrip(List<DeliveryOutput> packages, double maxCapacity, double speed)
        {
            var selected = new List<DeliveryOutput>();

            var all = GetAllSubsets(packages);

            foreach (var combo in all)
            {
                double totalWeight = combo.Sum(c => c.Weight);
                if (totalWeight > maxCapacity)
                    continue;

                if (combo.Count > selected.Count)
                {
                    selected = combo;
                    continue;
                }

                if (combo.Count < selected.Count)
                    continue;

                double comboWeight = combo.Sum(c => c.Weight);
                double bestWeight = selected.Sum(c => c.Weight);

                if (comboWeight > bestWeight)
                {
                    selected = combo;
                    continue;
                }

                if (comboWeight < bestWeight)
                    continue;

                double comboTime = combo.Max(c => c.Distance) / speed;
                double bestTime = selected.Max(c => c.Distance) / speed;

                if (comboTime < bestTime)
                {
                    selected = combo;
                }
            }

            return selected;
        }

        private List<List<DeliveryOutput>> GetAllSubsets(List<DeliveryOutput> packages)
        {
            var subsets = new List<List<DeliveryOutput>>();
            int n = packages.Count;

            for (int i = 1; i < (1 << n); i++)
            {
                var subset = new List<DeliveryOutput>();
                for (int j = 0; j < n; j++)
                {
                    if ((i & (1 << j)) > 0)
                        subset.Add(packages[j]);
                }
                subsets.Add(subset);
            }

            return subsets;
        }
    }
}
