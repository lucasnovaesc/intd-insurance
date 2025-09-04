
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

        public RabbitMqBackgroundConsumer(IRabbitMqSubscriber consumer, IServiceScopeFactory serviceScopeFactory)
        {
            _consumer = consumer;
            this._serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.StartConsumingAsync("proposal-queue", async message =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var processor = scope.ServiceProvider.GetRequiredService<IProposalProcessorService>();

                var proposal = JsonSerializer.Deserialize<string>(message);
                if (proposal != null)
                {
                    await processor.ProcessMessageAsync(proposal);
                }
            });

            await Task.Delay(-1, stoppingToken);
        }
    }
}
