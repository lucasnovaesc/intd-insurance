

namespace Service.UseCases.CustomerUseCase.Interfaces
{
    public interface IDeleteCustomerUseCase
    {
        public Task<bool> Delete(Guid customerId);
    }
}
