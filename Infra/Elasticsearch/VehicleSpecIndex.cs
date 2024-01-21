using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Aggregate;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Elasticsearch
{
    public class VehicleSpecIndex : IVehicleSpecIndex
    {
        private readonly IElasticClient elasticClient;

        public VehicleSpecIndex(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        public async Task IndexAsync(VehicleSpec vehicleSpec, CancellationToken cancellationToken)
        {
            var indexResponse = await elasticClient.IndexDocumentAsync(vehicleSpec, cancellationToken);
            if (!indexResponse.IsValid)
                throw new Exception($"Erro when index vehicle {vehicleSpec.Id}");
        }

        public async Task<IEnumerable<VehicleSpec>> SearchAsync(BasicSearchDto searchDto, CancellationToken cancellationToken)
        {
            var query = new QueryContainerDescriptor<VehicleSpec>();

            query.Match(m =>
            {
                if (!string.IsNullOrEmpty(searchDto.Brand))
                    m.Field(f => f.Brand).Operator(Operator.And).Query(searchDto.Brand);

                if (!string.IsNullOrEmpty(searchDto.Model))
                    m.Field(f => f.Model).Operator(Operator.And).Query(searchDto.Model);

                if (!string.IsNullOrEmpty(searchDto.ManufacturerYear))
                    m.Field(f => f.ManufacturingYear).Operator(Operator.And).Query(searchDto.ManufacturerYear);

                if (!string.IsNullOrEmpty(searchDto.ModelYear))
                    m.Field(f => f.ModelYear).Operator(Operator.And).Query(searchDto.ModelYear);

                return m;
            });

            var result = await elasticClient.SearchAsync<VehicleSpec>(s => s.Query(_ => query));

            return result?.Documents ?? [];
        }

        public async Task<IEnumerable<VehicleSpec>> SearchAsync(string textWithWildcards, CancellationToken cancellationToken)
        {
            var query = new QueryContainerDescriptor<VehicleSpec>()
                .Wildcard(w => w.Field(f => f.FullTextSearch).Value(textWithWildcards));

            var result = await elasticClient.SearchAsync<VehicleSpec>(s => s.Query(_ => query));

            return result?.Documents ?? [];
        }

        public async Task<VehicleSpec?> SearchByIdAsync(string id, CancellationToken cancellationToken)
        {
            var result = await elasticClient.GetAsync<VehicleSpec>(id);
            return result.Source ?? null;
        }
    }
}
