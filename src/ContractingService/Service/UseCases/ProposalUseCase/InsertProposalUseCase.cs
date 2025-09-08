

using Domain.Entities;
using Domain.Factories;
using Domain.Repository;
using Service.DataTransferObjects.ProposalDTO.Request;
using Service.UseCases.ProposalUseCase.Interfaces;

namespace Service.UseCases.ProposalUseCase
{
    public class InsertProposalUseCase : IInsertProposalUseCase
    {
        private readonly ProposalFactory _proposalFactory;
        private readonly IProposalRepository _proposalRepository;

        public InsertProposalUseCase(ProposalFactory proposalFactory, IProposalRepository proposalRepository)
        {
            this._proposalFactory = proposalFactory;
            this._proposalRepository = proposalRepository;
        }

        public async Task<bool> Insert(RequestInsertProposalDTO requestInsertProposalDTO)
        {
            try
            {
                Proposal newProposal = this._proposalFactory.MakeNew(requestInsertProposalDTO.ProposalNumber, requestInsertProposalDTO.ProductId, requestInsertProposalDTO.CustomerId);
                bool returnInsertProposal = await this._proposalRepository.Insert(newProposal);

                if (!returnInsertProposal)
                {
                    throw new Exception("It's not possible to insert Proposal");
                }
                return returnInsertProposal;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
