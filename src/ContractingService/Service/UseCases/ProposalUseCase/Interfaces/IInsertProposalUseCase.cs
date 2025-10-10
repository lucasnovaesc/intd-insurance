using Service.DataTransferObjects.ProposalDTO.Request;

namespace Service.UseCases.ProposalUseCase.Interfaces
{
    public interface IInsertProposalUseCase
    {
        Task<bool> Insert(RequestInsertProposalDTO requestInsertProposalDTO);
    }
}
