using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections;

namespace VehicleInventory.Domain.Enums
{
    public enum VehicleStatus
    {
        Available,
        Rented,
        Reserved,
        Serviced

    }
}
