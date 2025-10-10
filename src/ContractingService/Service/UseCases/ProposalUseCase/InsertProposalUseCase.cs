using Domain.Entities;
using Domain.Factories;
using Domain.Repositories;
using Service.DataTransferObjects.ProposalDTO.Request;
using Service.UseCases.ProposalUseCase.Interfaces;
using Infrastructure.Resources.RabbitMQ.Interfaces;

namespace Service.UseCases.ProposalUseCase
{
    public class InsertProposalUseCase : IInsertProposalUseCase
    {
        private readonly ProposalFactory _proposalFactory;
        private readonly IProposalRepository _proposalRepository;
        private readonly IRabbitMQClient _rabbitClient;

        public InsertProposalUseCase(
            ProposalFactory proposalFactory,
            IProposalRepository proposalRepository,
            IRabbitMQClient rabbitClient)
        {
            _proposalFactory = proposalFactory;
            _proposalRepository = proposalRepository;
            _rabbitClient = rabbitClient;
        }

        public async Task<bool> Insert(RequestInsertProposalDTO dto)
        {
            try
            {
                var proposal = _proposalFactory.MakeNew(dto.ProposalNumber, dto.ProductId, dto.CustomerId);
                bool inserted = await _proposalRepository.Insert(proposal);

                if (!inserted)
                    return false;

                var publishResult = await _rabbitClient.PublishAsync(dto, "proposal-queue");
                return publishResult.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir proposta: {ex.Message}");
                return false;
            }
        }
    }
}
