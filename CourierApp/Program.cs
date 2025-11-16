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
            try
            {
                var configData = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

                var serviceProvider = new ServiceCollection()
                    .Configure<OfferSettings>(configData.GetSection("OfferSettings"))
                    .AddSingleton<IInputService, InputService>()
                    .AddSingleton<IOutputService, OutputService>()
                    .AddSingleton<IOfferService, OfferService>()
                    .AddSingleton<ICostCalculation, DeliveryCostCalculationService>()
                    .AddSingleton<IDeliveryTime, DeliveryTimeCalculationService>()
                    .BuildServiceProvider();

                var input = serviceProvider.GetRequiredService<IInputService>();
                var output = serviceProvider.GetRequiredService<IOutputService>();
                var costService = serviceProvider.GetRequiredService<ICostCalculation>();
                var timeService = serviceProvider.GetRequiredService<IDeliveryTime>();

                var (baseCost, courierPackages, vehicleConfig) = input.GetInput();

                var costOutput = courierPackages.Select(pkg => costService.Calculate(pkg, baseCost)).ToList();
                var finalOutput = timeService.TimeCalculation(costOutput, vehicleConfig);

                output.Print(finalOutput);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Program ends with error message: " + ex.Message);
            }
        }
    }
}
