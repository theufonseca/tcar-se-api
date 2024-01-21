using AutoMapper;
using Domain.Aggregate;
using Infra.RabbitMQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Elasticsearch
{
    public class VehicleSpecMapper : Profile
    {
        public VehicleSpecMapper()
        {
            CreateMap<VehicleModel, VehicleSpec>();
        }
    }
}
