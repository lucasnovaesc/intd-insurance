

namespace Infrastruture.Resources.RabbitMQ.Interfaces
{
    public interface IRabbitMQClient
    {
        Task PublishAsync<T>(T message, string queueName);
    }
}
