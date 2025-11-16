using CourierApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp.Interfaces
{
    public interface IOutputService
    {
        void Print(List<DeliveryOutput> results);
    }
}
