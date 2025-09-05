

using Service.DataTransferObjects.ProposalDTO.Request;

namespace Service.UseCases.ProposalUseCase.Interfaces
{
    public interface IInsertProposalUseCase
    {
        public Task<bool> Insert(RequestInsertProposalDTO requestInsertProposalDTO);
    }
}
