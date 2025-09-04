

namespace Service.UseCases.ProposalUseCase.Interfaces
{
    public interface IProposalProcessorService
    {
        public Task ProcessMessageAsync(string message);
    }
}
