using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.RabbitMQ.Models
{
    internal class VehicleSpecCreated
    {
        public VehicleModel VehicleSpec { get; private set; }

        public VehicleSpecCreated(VehicleModel vehicleSpec)
        {
            VehicleSpec = vehicleSpec;
        }
    }
}
