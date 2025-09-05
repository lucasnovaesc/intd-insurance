

namespace Infrastructure.Resources.RabbitMq.Interfaces
{
    public interface IRabbitMqSubscriber
    {
        Task StartConsumingAsync(string queueName, Func<string, Task> onMessageReceived);
        string GetLastMessage();
    }
}
