using Application.Interfaces;
using Application.UseCases;
using AutoMapper;
using Domain.Aggregate;
using Infra.RabbitMQ.Models;
using MassTransit;
using MassTransit.Caching;
using MassTransit.Mediator;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infra.RabbitMQ.Consumers
{
    public class VehicleSpecConsumer : IDisposable
    {
        private readonly RabbitMQConnection connection;
        private readonly IMapper mapper;
        private readonly IVehicleSpecIndex vehicleSpecIndex;

        public VehicleSpecConsumer(RabbitMQConnection connection, IMapper mapper,
            IVehicleSpecIndex vehicleSpecIndex)
        {
            this.connection = connection;
            this.mapper = mapper;
            this.vehicleSpecIndex = vehicleSpecIndex;
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public async Task StartConsume()
        {
            var channel = connection.channel;
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var messages = Encoding.UTF8.GetString(body);
                var vehicleSpecCreated = JsonSerializer.Deserialize<VehicleSpecCreated>(messages);

                if (vehicleSpecCreated is null)
                    throw new ArgumentException("Error when consume vehicle message.");

                var vehicleSpec = vehicleSpecCreated.VehicleSpec.ToVehicleSpec();
                var request = new IndexVehicleSpecRequest(vehicleSpec);
                var requestHandler = new IndexVehicleSpecHandler(vehicleSpecIndex);
                await requestHandler.Handle(request, CancellationToken.None);
            };

            channel.BasicConsume(queue: "vehicleSpec_created", autoAck: true, consumer: consumer);
        }
    }
}
