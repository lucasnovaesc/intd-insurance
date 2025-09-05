

using Infrastruture.Resources.RabbitMQ.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastruture.Resources.RabbitMQ
{
    public class RabbitMQClient : IRabbitMQClient, IAsyncDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        public RabbitMQClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory() { HostName = _configuration["RabbitMqHost"], Port = int.Parse(_configuration["RabbitMqPort"]) };
            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
        }

        public async Task PublishAsync<T>(T message, string queueName)
        {
            await _channel.QueueDeclareAsync(queue: queueName,
                                             durable: true,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            var properties = new BasicProperties
            {
                DeliveryMode = DeliveryModes.Persistent // garante que a mensagem sobrevive restart do Rabbit
            };

            await _channel.BasicPublishAsync(exchange: "",
                                             routingKey: queueName,
                                             mandatory: false,
                                             basicProperties: properties,
                                             body: body);

            
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel is not null)
                await _channel.CloseAsync();

            if (_connection is not null)
                await _connection.CloseAsync();
        }
    }
}

