

using Service.DataTransferObjects.CustomerDTO.Response;

namespace Service.UseCases.CustomerUseCase.Interfaces
{
    public interface IReadCustomerUseCase
    {
        public Task<ResponseReadCustomerDTO> FindById(Guid customerId);
        public Task<List<ResponseReadCustomerDTO>> FindAll();
    }
}
