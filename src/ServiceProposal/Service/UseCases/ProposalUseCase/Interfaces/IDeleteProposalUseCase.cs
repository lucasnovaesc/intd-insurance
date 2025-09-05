
namespace Service.UseCases.ProposalUseCase.Interfaces
{
    public interface IDeleteProposalUseCase
    {
        public Task<bool> Delete(Guid proposalId);
    }
}
