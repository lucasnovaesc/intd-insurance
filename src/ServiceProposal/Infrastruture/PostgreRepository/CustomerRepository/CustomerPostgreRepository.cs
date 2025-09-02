

using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infrastruture.PostgreRepository.CustomerRepository
{
    public class CustomerPostgreRepository : ICustomerRepository
    {
        private readonly ServiceProposalContext _serviceProposalContext;
        private int _codeReturnDatabase = 0;
        public CustomerPostgreRepository(ServiceProposalContext ctx) => this._serviceProposalContext = ctx;

        public async Task<bool> Delete(Guid customerId)
        {
            try
            {
                Customer deleteCustomer = this._serviceProposalContext.Customers.FirstOrDefault(q => q.CustomerId == customerId);
                if (deleteCustomer == null)
                    throw new EntityNotFoundException($"{deleteCustomer.Name} not Found");

                this._serviceProposalContext.Customers.Remove(deleteCustomer);
                int returnDbChange = await this._serviceProposalContext.SaveChangesAsync();

                if (returnDbChange == 0)
                    throw new EntityNotFoundException($"Error: Can not deleted {deleteCustomer.Name}");

                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;
            }
            catch (DbUpdateException dbEx)
            {
                this._serviceProposalContext.Entry(customerId).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new EntityNotFoundException($"Erro to delete customer: {pgEx.Detail}");

                throw new EntityNotFoundException(dbEx.Message);
            }
        }

        public async Task<List<Customer>> FindAll()
        {
            try
            {
                List<Customer> customerList = await this._serviceProposalContext.Customers
                    .ToListAsync();
                return customerList;
            }
            catch
            {
                throw new Exception("Can not possible to find all Customers");
            }
        }

        public async Task<Customer> FindById(Guid customerId)
        {
            try
            {

                Customer customer = await this._serviceProposalContext.Customers
                    .FirstOrDefaultAsync(c => c.CustomerId == customerId);
                return customer;
            }
            catch
            {
                throw new Exception($"Can not possible to find Customer by unique identifier");
            }
        }

        public async Task<bool> Insert(Customer customer)
        {
            try
            {
                this._serviceProposalContext.Customers.Add(customer);
                int returnDbChange = await this._serviceProposalContext.SaveChangesAsync();


                if (returnDbChange <= _codeReturnDatabase)
                {
                    throw new InsertEntityException($"Error: Can not Insert {customer.Name}");
                }
                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;

            }
            catch (DbUpdateException dbEx)
            {
                this._serviceProposalContext.Entry(customer).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new InsertEntityException($"Error: Can not Insert {pgEx.Detail}");

                throw new InsertEntityException(dbEx.Message);
            }
        }

        public async Task<bool> Update(Customer customer)
        {
            try
            {
                Customer OldCustomer = this._serviceProposalContext.Customers.FirstOrDefault(x => x.CustomerId == customer.CustomerId);
                if (OldCustomer == null)
                    throw new EntityNotFoundException($"{customer.Name} not Found!");

                this._serviceProposalContext.Customers.Entry(OldCustomer).CurrentValues.SetValues(customer);
                int returnDbChange = await this._serviceProposalContext.SaveChangesAsync();

                if (returnDbChange == _codeReturnDatabase)
                    throw new UpdateEntityException($"Error: Can not Update {customer.Name}");

                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;
            }
            catch (DbUpdateException dbEx)
            {
                this._serviceProposalContext.Entry(customer).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new UpdateEntityException($"Error: Can not Update {pgEx.Detail}");

                throw new UpdateEntityException(dbEx.Message);
            }
        }
    }
}
