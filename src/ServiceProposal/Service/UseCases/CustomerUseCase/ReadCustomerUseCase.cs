

using Domain.Entities;
using Domain.Factories;
using Domain.Repositories;
using Service.DataTransferObjects.CustomerDTO.Response;
using Service.UseCases.CustomerUseCase.Interfaces;

namespace Service.UseCases.CustomerUseCase
{
    public class ReadCustomerUseCase : IReadCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerFactory _customerFactory;

        public ReadCustomerUseCase(ICustomerRepository customerRepository, CustomerFactory customerFactory)
        {
            this._customerRepository = customerRepository;
            this._customerFactory = customerFactory;
        }

        public async Task<List<ResponseReadCustomerDTO>> FindAll()
        {
            try
            {
                List<Customer> customerList = await this._customerRepository.FindAll();
                List<ResponseReadCustomerDTO> responseReadCustomerDTOList = new List<ResponseReadCustomerDTO>();
                foreach(Customer customer in customerList)
                {
                    ResponseReadCustomerDTO responseReadCustomerDTO = new ResponseReadCustomerDTO(
                        customer.CustomerId,
                        customer.Name,
                        customer.Email,
                        customer.PhoneNumber,
                        customer.Address,
                        customer.DateCreation);
                    responseReadCustomerDTOList.Add(responseReadCustomerDTO);
                }
                return responseReadCustomerDTOList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseReadCustomerDTO> FindById(Guid customerId)
        {
            try
            {
                Customer customer = await this._customerRepository.FindById(customerId);

                ResponseReadCustomerDTO responseReadCustomerDTO = new ResponseReadCustomerDTO(
                    customer.CustomerId,
                    customer.Name,
                    customer.Email,
                    customer.PhoneNumber,
                    customer.Address,
                    customer.DateCreation);

                return responseReadCustomerDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
