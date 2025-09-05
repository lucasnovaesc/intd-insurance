using Service.DataTransferObjects.CustomerDTO.Request;

namespace Service.UseCases.CustomerUseCase.Interfaces
{
    public interface IInsertCustomerUseCase
    {
        public Task<bool> Insert(RequestInsertCustomerDTO requestInsertCustomerDTO);
    }
}
