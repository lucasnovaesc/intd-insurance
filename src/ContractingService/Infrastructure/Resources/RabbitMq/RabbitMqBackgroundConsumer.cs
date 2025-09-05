
using Microsoft.Extensions.Hosting;
using Infrastructure.Resources.RabbitMq.Interfaces;
using Service.UseCases.ProposalUseCase.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;


namespace Infrastructure.Resources.RabbitMq
{
    public class RabbitMqBackgroundConsumer : BackgroundService
    {
        private readonly IRabbitMqSubscriber _consumer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMessageStore _messageStore;

        public RabbitMqBackgroundConsumer(IRabbitMqSubscriber consumer, IServiceScopeFactory serviceScopeFactory, IMessageStore messageStore)
        {
            _consumer = consumer;
            this._serviceScopeFactory = serviceScopeFactory;
            _messageStore = messageStore;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.StartConsumingAsync("hello", async message =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var processor = scope.ServiceProvider.GetRequiredService<IProposalProcessorService>();

                var proposal = JsonSerializer.Deserialize<string>(message);
                if (proposal != null)
                {
                    await processor.ProcessMessageAsync(proposal);

                    // guarda a última mensagem recebida
                    _messageStore.SetLastMessage(proposal);
                }
            });

            await Task.Delay(-1, stoppingToken);
        }
    }
}
