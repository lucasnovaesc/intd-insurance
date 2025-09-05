

using Service.DataTransferObjects.ProposalDTO.Request;

namespace Service.UseCases.ProposalUseCase.Interfaces
{
    public interface IUpdateProposalUseCase
    {
        public Task<bool> Update(RequestUpdateProposalDTO requestUpdateProposalDTO);
    }
}
