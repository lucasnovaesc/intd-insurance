
using Domain.Entities;
using Domain.Factories;
using Domain.Repositories;
using Service.DataTransferObjects.CustomerDTO.Request;
using Service.UseCases.CustomerUseCase.Interfaces;

namespace Service.UseCases.CustomerUseCase
{
    public class InsertCustomerUseCase : IInsertCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerFactory _customerFactory;

        public InsertCustomerUseCase(ICustomerRepository customerRepository, CustomerFactory customerFactory)
        {
            this._customerRepository = customerRepository;
            this._customerFactory = customerFactory;
        }

        public async Task<bool> Insert(RequestInsertCustomerDTO requestInsertCustomerDTO)
        {
            try
            {
                Customer newCustomer = this._customerFactory.MakeNew(requestInsertCustomerDTO.Name, requestInsertCustomerDTO.Email, requestInsertCustomerDTO.PhoneNumber, requestInsertCustomerDTO.Address);
                bool returnInsertCustomer = await this._customerRepository.Insert(newCustomer);
                if(!returnInsertCustomer)
                {
                    throw new Exception("It's not possible to insert Customer");
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
