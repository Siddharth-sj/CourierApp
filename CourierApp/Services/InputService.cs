using CourierApp.Interfaces;
using CourierApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp.Services
{
    public class InputService : IInputService
    {
        public (double baseCost, List<CourierPackage> packages, VehSettings vehicles) GetInput()
        {
            // Base cost + package count
            Console.WriteLine("Enter base_delivery_cost and no_of_packages:");
            var inp = Console.ReadLine()?.Split(' ');

            if (inp == null || inp.Length != 2)
                throw new FormatException("Invalid input format for base cost and number of packages.");

            if (!double.TryParse(inp?[0], out double baseCost))
                throw new FormatException("Invalid base cost.");

            if (!int.TryParse(inp[1], out int numPack))
                throw new FormatException("Invalid number of packages.");

            // Read packages
            var courierPackages = new List<CourierPackage>();
            Console.WriteLine("Enter package details (pkg_id weight distance offer_code):");

            for (int i = 0; i < numPack; i++)
            {
                var inpPck = Console.ReadLine()?.Split(' ');
                if (inpPck == null || inpPck.Length != 4)
                {
                    Console.WriteLine("Invalid package format.! Please add proper package details");
                    i--;
                    continue;
                }

                if (!double.TryParse(inpPck[1], out double weight) ||
                    !double.TryParse(inpPck[2], out double distance))
                {
                    Console.WriteLine("Invalid package weight or distance. Please add proper weight and distance of the package.");
                    i--;
                    continue;
                }

                courierPackages.Add(new CourierPackage
                {
                    Id = inpPck[0],
                    Weight = weight,
                    Dist = distance,
                    OfferCode = inpPck[3]
                });
            }

            // Vehicle input
            Console.WriteLine("Enter vehicle info (no_of_vehicles max_speed max_carriable_weight):");
            var vehInput = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (vehInput == null || vehInput.Length != 3)
                throw new FormatException("Invalid vehicle input.");

            return (baseCost, courierPackages, new VehSettings
            {
                NumOfVehicles = int.Parse(vehInput[0]),
                MaxSpeed = double.Parse(vehInput[1]),
                MaxWeight = double.Parse(vehInput[2])
            });
        }
    }
}
