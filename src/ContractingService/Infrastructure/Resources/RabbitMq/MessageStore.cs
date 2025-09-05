

namespace Infrastructure.Resources.RabbitMq
{
    public interface IMessageStore
    {
        void SetLastMessage(string message);
        string? GetLastMessage();
    }

    public class InMemoryMessageStore : IMessageStore
    {
        private string? _lastMessage;

        public void SetLastMessage(string message)
        {
            _lastMessage = message;
        }

        public string? GetLastMessage() => _lastMessage;
    }
}
