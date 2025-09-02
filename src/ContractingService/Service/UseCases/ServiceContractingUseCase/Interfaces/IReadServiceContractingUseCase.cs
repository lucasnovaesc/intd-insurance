
using Service.DataTransferObjects.ServiceContractingDTO.Response;

namespace Service.UseCases.ServiceContractingUseCase.Interfaces
{
    public interface IReadServiceContractingUseCase
    {
        public Task<List<ResponseReadServiceContractingDTO>> FindAll();
        public Task<ResponseReadServiceContractingDTO> FindById(Guid serviceContractingId);

        public Task<ResponseReadServiceContractingDTO> FindByProposalId(Guid proposalId);
    }
}
