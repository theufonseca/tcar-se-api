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
    public record SearchVehicleByIdRequest(string Id) : IRequest<SearchVehicleByIdResponse>;

    public record SearchVehicleByIdResponse(VehicleSpec VehicleSpecs);

    public class SearchVehicleByIdRequestHandler : IRequestHandler<SearchVehicleByIdRequest, SearchVehicleByIdResponse>
    {
        private readonly IVehicleSpecIndex vehicleSpecIndex;

        public SearchVehicleByIdRequestHandler(IVehicleSpecIndex vehicleSpecIndex)
        {
            this.vehicleSpecIndex = vehicleSpecIndex;
        }

        public async Task<SearchVehicleByIdResponse> Handle(SearchVehicleByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await vehicleSpecIndex.SearchByIdAsync(request.Id, cancellationToken);

            if (result is null)
                throw new Exception($"Vehicle with id {request.Id} not found");

            return new SearchVehicleByIdResponse(result);
        }
    }
}
