

using Domain.Entities;
using Domain.Factories;
using Domain.Repositories;
using Service.UseCases.ProposalUseCase.Interfaces;

namespace Service.UseCases.ProposalUseCase
{
    public class DeleteProposalUseCase : IDeleteProposalUseCase
    {
        private readonly ProposalFactory _proposalFactory;
        private readonly IProposalRepository _proposalRepository;

        public DeleteProposalUseCase(ProposalFactory proposalFactory, IProposalRepository proposalRepository)
        {
            this._proposalFactory = proposalFactory;
            this._proposalRepository = proposalRepository;
        }

        public async Task<bool> Delete(Guid proposalId)
        {
            try
            {
                Proposal existentProposal = await this._proposalRepository.FindById(proposalId);
                if (existentProposal == null)
                {
                    throw new Exception("It's not possible to find Proposal");
                }
                bool returnDeleteProposal = await this._proposalRepository.Delete(proposalId);
                if (!returnDeleteProposal)
                {
                    throw new Exception("It's not possible to delete Proposal");
                }
                return returnDeleteProposal;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
