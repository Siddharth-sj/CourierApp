using CourierApp.Models;
using CourierApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp.Services
{
    public class OutputService : IOutputService
    {
        public void Print(List<DeliveryOutput> results)
        {
            Console.WriteLine("\nOutput: pkg_id Discount Total_Cost Estimated_Delivery_TimeInHours");

            foreach (var r in results)
            {
                Console.WriteLine($"{r.PackId} {r.Disc} {r.TotalCost} {r.EstDelTime}");
            }
        }
    }
}
