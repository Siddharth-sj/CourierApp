using CourierApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp.Interfaces
{
    public interface IInputService
    {
        (double baseCost, List<CourierPackage> packages, VehSettings vehicles) GetInput();
    }
}
