using Application.Dtos;
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
    public record BasicSearchVehicleRequest(BasicSearchDto SearchDto) : IRequest<BasicSearchVehicleResponse>;
    public record BasicSearchVehicleResponse(IEnumerable<VehicleSpec> VehicleSpecs);

    public class SearchVehicleRequestHandler : IRequestHandler<BasicSearchVehicleRequest, BasicSearchVehicleResponse>
    {
        private readonly IVehicleSpecIndex vehicleSpecIndex;

        public SearchVehicleRequestHandler(IVehicleSpecIndex vehicleSpecIndex)
        {
            this.vehicleSpecIndex = vehicleSpecIndex;
        }

        public async Task<BasicSearchVehicleResponse> Handle(BasicSearchVehicleRequest request, CancellationToken cancellationToken)
        {
            if(request.SearchDto is null)
                throw new ArgumentNullException(nameof(request.SearchDto));

            var result = await vehicleSpecIndex.SearchAsync(request.SearchDto, cancellationToken);
            return new BasicSearchVehicleResponse(result);
        }
    }
}
