

using Domain.Entities;
using Domain.Repository;
using Infrastructure.Resources.RabbitMq.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.UseCases.ProposalUseCase.Interfaces;
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

                        ProposalDTO proposalDTO = JsonSerializer.Deserialize<ProposalDTO>(message);

                        if (proposalDTO != null)
                        {
                            Proposal proposal = new Proposal (new Guid(proposalDTO.ProposalId), proposalDTO.ProposalNumber,
                                new Guid(proposalDTO.CustomerId), new Guid(proposalDTO.ProductId), proposalDTO.DateCreation,
                                proposalDTO.DateModification);
                            
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

                await Task.Delay(-1, stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no consumer: {ex}");
            }
        }
    }
}
