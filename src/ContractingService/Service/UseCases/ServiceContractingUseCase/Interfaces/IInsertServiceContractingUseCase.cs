

using Service.DataTransferObjects.ServiceContractingDTO.Request;

namespace Service.UseCases.ServiceContractingUseCase.Interfaces
{
    public interface IInsertServiceContractingUseCase
    {
        public Task<bool> Insert(RequestInsertServiceContractingDTO requestInsertServiceContractingDTO);
    }
}
