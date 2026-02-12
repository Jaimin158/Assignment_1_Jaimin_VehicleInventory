using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections;

namespace VehicleInventory.Domain.Enums
{
    internal enum VehicleStatus
    {
        Available=0,
        Rented=1,
        Reserved=2,
        Serviced=3

    }
}
