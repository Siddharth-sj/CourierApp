using CourierApp.Interfaces;
using CourierApp.Models;
using CourierApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CourierApp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var configData = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var serviceProvider = new ServiceCollection()
                .Configure<OfferSettings>(configData.GetSection("OfferSettings"))
                .AddSingleton<IOfferService, OfferService>()
                .AddSingleton<ICostCalculation, DeliveryCostCalculationService>()
                .AddSingleton<IDeliveryTime, DeliveryTimeCalculationService>()
                .BuildServiceProvider();

            var cal = serviceProvider.GetRequiredService<ICostCalculation>();
            var del = serviceProvider.GetRequiredService<IDeliveryTime>();

            Console.WriteLine("Enter base_delivery_cost and no_of_packages (e.g., '100 3'):");
            var inp = Console.ReadLine()?.Split(' ');
            double baseCost = double.Parse(inp[0]);
            int numPack = int.Parse(inp[1]);

            var courierPackages = new List<CourierPackage>();
            

            Console.WriteLine("Enter package details (pkg_id weight distance offer_code):");
            for (int i = 0; i < numPack; i++)
            {
                var inpPck = Console.ReadLine()?.Split(' ');
                if (inpPck == null || inpPck.Length < 4 ||
                    !double.TryParse(inpPck[1], out double weight) ||
                    !double.TryParse(inpPck[2], out double distance))
                {
                    Console.WriteLine("Invalid package input, try again!");
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

            Console.WriteLine("Enter vehicle info (no_of_vehicles max_speed max_carriable_weight):");
            var vehInput = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var vehicleConfig = new VehSettings
            {
                NumOfVehicles = int.Parse(vehInput[0]),
                MaxSpeed = double.Parse(vehInput[1]),
                MaxWeight = double.Parse(vehInput[2])
            };

            //Package cost after discounts
            var costOutput = courierPackages.Select(pkg => cal.Calculate(pkg, baseCost)).ToList();

            //Package estimate delivery times
            var finalOutput = del.TimeCalculation(costOutput, vehicleConfig);

            Console.WriteLine("\nOutput: pkg_id Discount Total_Cost Estimated_Delivery_TimeInHours");
            foreach (var result in finalOutput)
            {
                Console.WriteLine($"{result.PackId} {result.Disc} {result.TotalCost} {result.EstDelTime}");
            }
        }
    }
}
