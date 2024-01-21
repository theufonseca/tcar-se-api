using Application.Interfaces;
using Domain.Aggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public record IndexVehicleSpecRequest(VehicleSpec VehicleSpec) : IRequest<IndexVehicleSpecResponse>;
    public record IndexVehicleSpecResponse(bool Success);

    public class IndexVehicleSpecHandler : IRequestHandler<IndexVehicleSpecRequest, IndexVehicleSpecResponse>
    {
        private readonly IVehicleSpecIndex vehicleSpecIndex;

        public IndexVehicleSpecHandler(IVehicleSpecIndex vehicleSpecIndex)
        {
            this.vehicleSpecIndex = vehicleSpecIndex;
        }

        public async Task<IndexVehicleSpecResponse> Handle(IndexVehicleSpecRequest request, CancellationToken cancellationToken)
        {
            if (request.VehicleSpec is null)
                throw new ArgumentException("VehicleSpec must be filled!");

            if (!request.VehicleSpec.IsValid())
                throw new ArgumentException("VehicleSpec is not valid!");

            await vehicleSpecIndex.IndexAsync(request.VehicleSpec, cancellationToken);

            return new IndexVehicleSpecResponse(true);
        }
    }
}
