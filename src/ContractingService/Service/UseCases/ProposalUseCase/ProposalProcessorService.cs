using Domain.Interfaces; // ✅ Mudar para Domain
using System.Text.Json;

namespace Service.UseCases.ProposalUseCase
{
    public class ProposalProcessorService : IProposalProcessorService
    {
        public async Task<string> ProcessMessageAsync(string message)
        {
            var proposal = JsonSerializer.Deserialize<object>(message);
            Console.WriteLine($"📩 Processando proposta: {proposal}");
            await Task.CompletedTask;
            return proposal?.ToString() ?? "Mensagem vazia";
        }
    }
}
