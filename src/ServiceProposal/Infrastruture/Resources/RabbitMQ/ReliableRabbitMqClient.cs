using Infrastruture.Resources.RabbitMQ.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infrastruture.Resources.RabbitMQ
{
    public class ReliableRabbitMqClient : IRabbitMQClient, IAsyncDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly Timer _retryTimer;
        private readonly string _filePath;

        public ReliableRabbitMqClient(IConfiguration configuration)
        {
            _configuration = configuration;

            var host = _configuration["RabbitMqHost"] ?? "localhost";
            var port = int.Parse(_configuration["RabbitMqPort"] ?? "5672");

            var factory = new ConnectionFactory()
            {
                HostName = host,
                Port = port
            };

            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

            _filePath = Path.Combine("/app/data", "pendingMessages.json");

            // Timer 15 sec
            _retryTimer = new Timer(async _ => await RetryPendingMessages(),null,TimeSpan.Zero,TimeSpan.FromSeconds(15));
        }

        public async Task PublishAsync<T>(T message, string queueName)
        {
            try
            {
                await _channel.QueueDeclareAsync(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false
                );

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                var props = new BasicProperties { DeliveryMode = DeliveryModes.Persistent };

                await _channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: queueName,
                    mandatory: false,
                    basicProperties: props,
                    body: body
                );

                Console.WriteLine($"[OK] Mensagem publicada em {queueName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] Falha ao enviar ? salvando local: {ex.Message}");
                await SaveMessageToFile(message, queueName);
            }
        }

        private async Task SaveMessageToFile<T>(T message, string queueName)
        {
            List<PendingMessage> pending = new();

            if (File.Exists(_filePath))
            {
                var json = await File.ReadAllTextAsync(_filePath);
                if (!string.IsNullOrWhiteSpace(json))
                    pending = JsonSerializer.Deserialize<List<PendingMessage>>(json) ?? new();
            }

            pending.Add(new PendingMessage
            {
                Id = Guid.NewGuid().ToString(),
                QueueName = queueName,
                Message = JsonSerializer.Serialize(message)
            });

            await File.WriteAllTextAsync(
                _filePath,
                JsonSerializer.Serialize(pending, new JsonSerializerOptions { WriteIndented = true })
            );
        }

        private async Task RetryPendingMessages()
        {
            if (!File.Exists(_filePath)) return;

            var json = await File.ReadAllTextAsync(_filePath);
            if (string.IsNullOrWhiteSpace(json)) return;

            var pending = JsonSerializer.Deserialize<List<PendingMessage>>(json) ?? new();

            var enviados = new List<PendingMessage>();

            foreach (var item in pending)
            {
                try
                {
                    await _channel.QueueDeclareAsync(item.QueueName, true, false, false);

                    var body = Encoding.UTF8.GetBytes(item.Message);
                    var props = new BasicProperties { DeliveryMode = DeliveryModes.Persistent };

                    await _channel.BasicPublishAsync("", item.QueueName, false, props, body);

                    Console.WriteLine($"[RETRY-OK] Mensagem {item.Id} reenviada");
                    enviados.Add(item);
                }
                catch
                {
                    Console.WriteLine($"[RETRY-FAIL] Mensagem {item.Id} ainda não enviada");
                }
            }

            // Remove as mensagens reenviadas com sucesso
            if (enviados.Any())
            {
                var restantes = pending.Except(enviados).ToList();
                await File.WriteAllTextAsync(
                    _filePath,
                    JsonSerializer.Serialize(restantes, new JsonSerializerOptions { WriteIndented = true })
                );
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel is not null)
                await _channel.CloseAsync();

            if (_connection is not null)
                await _connection.CloseAsync();

            _retryTimer?.Dispose();

        }

        public class PendingMessage
        {
            public string Id { get; set; }
            public string QueueName { get; set; }
            public string Message { get; set; }
        }
    }
}