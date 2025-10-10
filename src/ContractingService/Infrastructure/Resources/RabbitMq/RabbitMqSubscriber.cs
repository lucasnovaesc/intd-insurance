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
        private IConnection _connection;
        private IChannel _channel;
        private static string _lastMessage = string.Empty;

        public RabbitMqSubscriber(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task StartConsumingAsync(string queueName, Func<string, Task> onMessageReceived)
        {
            try
            {
                if (_connection == null || _channel == null || _connection.IsOpen == false)
                {
                    var factory = new ConnectionFactory()
                    {
                        HostName = _configuration["RabbitMqHost"],
                        Port = int.Parse(_configuration["RabbitMqPort"])
                    };

                    _connection = await factory.CreateConnectionAsync();
                    _channel = await _connection.CreateChannelAsync();
                }

                await _channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new AsyncEventingBasicConsumer(_channel);

                consumer.ReceivedAsync += async (object model, BasicDeliverEventArgs ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    _lastMessage = message;

                    Console.WriteLine($"Mensagem recebida no subscriber: {message}");
                    if (onMessageReceived != null)
                        await onMessageReceived(message);
                };

                await _channel.BasicConsumeAsync(queueName, autoAck: true, consumer);
            }
            catch (Exception ex)
            {
                // Apenas loga, não derruba o serviço
                Console.WriteLine($"[WARN] Não foi possível conectar ao RabbitMQ ({ex.Message}). Continuando sem fila...");
            }
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
