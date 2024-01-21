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
    public record FullTextSearchVehicleRequest(string textWithWildcards) : IRequest<FullTextSearchVehicleResponse>;
    public record FullTextSearchVehicleResponse(IEnumerable<VehicleSpec> VehicleSpecs);

    public class FullTextSearchVehicleRequestHandler : IRequestHandler<FullTextSearchVehicleRequest, FullTextSearchVehicleResponse>
    {
        private readonly IVehicleSpecIndex vehicleSpecIndex;

        public FullTextSearchVehicleRequestHandler(IVehicleSpecIndex vehicleSpecIndex)
        {
            this.vehicleSpecIndex = vehicleSpecIndex;
        }

        public async Task<FullTextSearchVehicleResponse> Handle(FullTextSearchVehicleRequest request, CancellationToken cancellationToken)
        {
            var result = await vehicleSpecIndex.SearchAsync(request.textWithWildcards, cancellationToken);
            return new FullTextSearchVehicleResponse(result);
        }
    }
}
