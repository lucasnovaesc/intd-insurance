
using Service.DataTransferObjects.ServiceContractingDTO.Request;

namespace Service.UseCases.ServiceContractingUseCase.Interfaces
{
    public interface IUpdateServiceContractingUseCase
    {
        public Task<bool> Update(RequestUpdateServiceContractingDTO requestUpdateServiceContractingDTO);
    }
}
