using Infra.RabbitMQ.Config;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.RabbitMQ
{
    public class RabbitMQConnection : IDisposable
    {
        private IConnection connection;
        public IModel channel;

        public RabbitMQConnection(IOptions<RabbitMQConfig> options)
        {
            var factory = new ConnectionFactory()
            {
                HostName = options.Value.Host,
                Port = Convert.ToInt32(options.Value.Port)
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: options.Value.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void Dispose()
        {
            connection.Dispose();
            channel.Dispose();
        }
    }
}
