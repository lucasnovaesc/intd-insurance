

using Service.DataTransferObjects.ProposalDTO.Response;

namespace Service.UseCases.ProposalUseCase.Interfaces
{
    public interface IReadProposalUseCase
    {
        public Task<ResponseReadProposalDTO> FindById(Guid proposalId);
        public Task<ResponseReadProposalDTO> FindByProposalNumber(long proposalNumber);
        public Task<List<ResponseReadProposalDTO>> FindAll();
        public Task<List<ResponseReadProposalDTO>> FindByCustomerId(Guid customerId);
    }
}
