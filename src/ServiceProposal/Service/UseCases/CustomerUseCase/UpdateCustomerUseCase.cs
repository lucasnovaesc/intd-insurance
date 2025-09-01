
using Domain.Entities;
using Domain.Factories;
using Domain.Repositories;
using Service.DataTransferObjects.CustomerDTO.Request;
using Service.UseCases.CustomerUseCase.Interfaces;

namespace Service.UseCases.CustomerUseCase
{
    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerFactory _customerFactory;

        public UpdateCustomerUseCase(ICustomerRepository customerRepository, CustomerFactory customerFactory)
        {
            this._customerRepository = customerRepository;
            this._customerFactory = customerFactory;
        }

        public async Task<bool> Update(RequestUpdateCustomerDTO requestUpdateCustomerDTO)
        {
            try
            {
                Customer existentCustomer = this._customerFactory.MakeExistent(requestUpdateCustomerDTO.CustomerId, requestUpdateCustomerDTO.Name, requestUpdateCustomerDTO.Email,
                                                                    requestUpdateCustomerDTO.PhoneNumber, requestUpdateCustomerDTO.Address, requestUpdateCustomerDTO.DateCreation);
                bool returnInsertCustomer = await this._customerRepository.Update(existentCustomer);
                if (!returnInsertCustomer)
                {
                    throw new Exception("It's not possible to Update Customer");
                }
                return returnInsertCustomer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
