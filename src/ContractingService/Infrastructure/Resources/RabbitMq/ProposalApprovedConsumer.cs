using Domain.Entities;
using Domain.Repository;
using Infrastructure.Resources.RabbitMq.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace Infrastructure.Resources.RabbitMq
{
    public class ProposalApprovedConsumer : BackgroundService
    {
        private readonly IRabbitMqSubscriber _consumer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMessageStore _messageStore;

        public ProposalApprovedConsumer(IRabbitMqSubscriber consumer, IServiceScopeFactory serviceScopeFactory, IMessageStore messageStore)
        {
            _consumer = consumer;
            this._serviceScopeFactory = serviceScopeFactory;
            _messageStore = messageStore;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                Console.WriteLine("Consumer iniciado, aguardando mensagens...");

                await _consumer.StartConsumingAsync("proposal.approved", async message =>
                {
                    try
                    {
                        Console.WriteLine($"Mensagem recebida: {message}");

                        using var scope = _serviceScopeFactory.CreateScope();
                        var repository = scope.ServiceProvider.GetRequiredService<IProposalRepository>();

                        // Desserializar para um tipo dinâmico ou criar um DTO simples
                        var proposalData = JsonSerializer.Deserialize<ProposalData>(message);

                        if (proposalData != null)
                        {
                            Proposal proposal = new Proposal(
                                new Guid(proposalData.ProposalId),
                                proposalData.ProposalNumber,
                                new Guid(proposalData.CustomerId),
                                new Guid(proposalData.ProductId),
                                proposalData.DateCreation,
                                proposalData.DateModification
                            );

                            Console.WriteLine($"Proposta desserializada: {proposal.ProposalNumber}, {proposal.ProductId}");
                            await repository.Insert(proposal);
                        }
                        else
                        {
                            Console.WriteLine("Falha ao desserializar a proposta.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao processar mensagem: {ex}");
                    }
                });

                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Consumer encerrado com cancelamento (parada do container).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no consumer: {ex}");
            }
        }

        // Classe interna para desserialização
        private class ProposalData
        {
            public string ProposalId { get; set; } = string.Empty;
            public long ProposalNumber { get; set; }
            public string CustomerId { get; set; } = string.Empty;
            public string ProductId { get; set; } = string.Empty;
            public DateTime DateCreation { get; set; }
            public DateTime DateModification { get; set; }
        }
    }
}
