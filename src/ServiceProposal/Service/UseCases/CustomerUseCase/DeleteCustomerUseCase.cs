

using Domain.Entities;
using Domain.Repositories;
using Service.UseCases.CustomerUseCase.Interfaces;

namespace Service.UseCases.CustomerUseCase
{
    public class DeleteCustomerUseCase : IDeleteCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerUseCase(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }

        public async Task<bool> Delete(Guid customerId)
        {
            try
            {
                Customer existentCustomer = await this._customerRepository.FindById(customerId);
                if(existentCustomer == null)
                {
                    throw new Exception("it's not possible to find customer");
                }
                bool returnDeleteCustomer = await this._customerRepository.Delete(customerId);
                if(returnDeleteCustomer)
                {
                    throw new Exception("it's not possible to delete customer");
                }
                return returnDeleteCustomer;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
