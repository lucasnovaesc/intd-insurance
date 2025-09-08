

using Domain.Entities;
using Domain.Factories;
using Domain.Repositories;
using Infrastruture.Resources.RabbitMQ.Interfaces;
using Service.DataTransferObjects.ProposalDTO.Request;
using Service.UseCases.ProposalUseCase.Interfaces;
using System.Text.Json;

namespace Service.UseCases.ProposalUseCase
{
    public class UpdateProposalUseCase : IUpdateProposalUseCase
    {
        private readonly ProposalFactory _proposalFactory;
        private readonly IProposalRepository _proposalRepository;
        private readonly IRabbitMQClient _rabbitMQClient;

        public UpdateProposalUseCase(ProposalFactory proposalFactory, IProposalRepository proposalRepository, IRabbitMQClient rabbitMQClient)
        {
            this._proposalFactory = proposalFactory;
            this._proposalRepository = proposalRepository;
            this._rabbitMQClient = rabbitMQClient;
        }

        public async Task<bool> Update(RequestUpdateProposalDTO requestUpdateProposalDTO)
        {
            try
            {
                Proposal existentProposal = this._proposalFactory.MakeExistent(requestUpdateProposalDTO.ProposalId, requestUpdateProposalDTO.ProposalNumber, requestUpdateProposalDTO.ProductId,
                                                                                requestUpdateProposalDTO.CustomerId, requestUpdateProposalDTO.DateCreation, requestUpdateProposalDTO.ProsposalStatus);

                bool returnUpdateProposal = await this._proposalRepository.Update(existentProposal);
                if (!returnUpdateProposal)
                {
                    throw new Exception("It's not possible to update Proposal");
                }
                Console.WriteLine($"Status da proposta: {existentProposal.ProposalStatusId}, publicando na fila...");
                if (existentProposal.ProposalStatusId.ToString() == "Approved")
                {
                    await this.ApproveProposalAsync(existentProposal);
                }
                return returnUpdateProposal;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task ApproveProposalAsync(Proposal proposal)
        {
            if (proposal == null)
                throw new Exception("Proposal not found");

            await this._rabbitMQClient.PublishAsync(proposal, "proposal.approved");
        }
    }
}
