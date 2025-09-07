

using Infrastructure.Resources.RabbitMq.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Infrastructure.Resources.RabbitMq
{
    public class RabbitMqSubscriber : IRabbitMqSubscriber, IAsyncDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private static string _lastMessage = string.Empty;

        public RabbitMqSubscriber(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory() { HostName = _configuration["RabbitMqHost"], Port = int.Parse(_configuration["RabbitMqPort"]) };
            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
        }

        public async Task StartConsumingAsync(string queueName, Func<string, Task> onMessageReceived)
        {

            await _channel.QueueDeclareAsync(queueName, true, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (object model, BasicDeliverEventArgs ea) =>
            {
                byte[] body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                _lastMessage = message;

                Console.WriteLine($"Mensagem recebida no subscriber: {message}");
                if (onMessageReceived != null)
                    await onMessageReceived(message);
            };

            await _channel.BasicConsumeAsync(queueName, autoAck: true, consumer);
        }

        public string GetLastMessage() => _lastMessage;

        public async ValueTask DisposeAsync()
        {
            if (_channel != null)
                await _channel.CloseAsync();

            if (_connection != null)
                await _connection.CloseAsync();
        }
    }
}
