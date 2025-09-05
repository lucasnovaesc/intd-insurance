

using Domain.Entities;
using Domain.Factories;
using Domain.Repositories;
using Service.DataTransferObjects.ProposalDTO.Request;
using Service.UseCases.ProposalUseCase.Interfaces;

namespace Service.UseCases.ProposalUseCase
{
    public class UpdateProposalUseCase : IUpdateProposalUseCase
    {
        private readonly ProposalFactory _proposalFactory;
        private readonly IProposalRepository _proposalRepository;

        public UpdateProposalUseCase(ProposalFactory proposalFactory, IProposalRepository proposalRepository)
        {
            this._proposalFactory = proposalFactory;
            this._proposalRepository = proposalRepository;
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
                return returnUpdateProposal;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
