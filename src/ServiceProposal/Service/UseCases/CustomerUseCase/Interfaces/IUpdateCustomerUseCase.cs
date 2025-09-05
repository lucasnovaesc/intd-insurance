


using Service.DataTransferObjects.CustomerDTO.Request;

namespace Service.UseCases.CustomerUseCase.Interfaces
{
    public interface IUpdateCustomerUseCase
    {
        public Task<bool> Update(RequestUpdateCustomerDTO requestUpdateCustomerDTO);
    }
}
