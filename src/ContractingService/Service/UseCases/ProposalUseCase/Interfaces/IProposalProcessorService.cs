

namespace Service.UseCases.ProposalUseCase.Interfaces
{
    public interface IProposalProcessorService
    {
        public Task<string> ProcessMessageAsync(string message);
    }
}
