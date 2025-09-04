

using Service.UseCases.ProposalUseCase.Interfaces;
using System.Text.Json;

namespace Service.UseCases.ProposalUseCase
{
    public class ProposalProcessorService : IProposalProcessorService
    {
        public async Task ProcessMessageAsync(string message)
        {
            var proposal = JsonSerializer.Deserialize<object>(message);

            Console.WriteLine($"📩 Processando proposta: {proposal} - {proposal:C}");

            // Aqui você poderia salvar no banco, enviar notificação, etc.
            await Task.CompletedTask;
        }
    }
}
