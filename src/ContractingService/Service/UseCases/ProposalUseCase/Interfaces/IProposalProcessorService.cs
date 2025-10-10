using Service.DataTransferObjects.ProposalDTO.Request;

 namespace Service.UseCases.ProposalUseCase.Interfaces
{
    public interface IProposalProcessorService
    {
        Task<string> ProcessMessageAsync(string message);
    }
}
