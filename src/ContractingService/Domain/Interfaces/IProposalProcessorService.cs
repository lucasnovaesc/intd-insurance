namespace Domain.Interfaces
{
    public interface IProposalProcessorService
    {
        Task<string> ProcessMessageAsync(string message);
    }
}
