using Application.Dtos;
using Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVehicleSpecIndex
    {
        Task IndexAsync(VehicleSpec vehicleSpec, CancellationToken cancellationToken);
        Task<IEnumerable<VehicleSpec>> SearchAsync(BasicSearchDto searchDto, CancellationToken cancellationToken);
        Task<IEnumerable<VehicleSpec>> SearchAsync(string textWithWildcards, CancellationToken cancellationToken);
        Task<VehicleSpec?> SearchByIdAsync(string id, CancellationToken cancellationToken);
    }
}
