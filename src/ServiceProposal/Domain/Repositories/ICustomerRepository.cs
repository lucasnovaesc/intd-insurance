

using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICustomerRepository
    {
        public Task<bool> Insert(Customer customer);
        public Task<bool> Update(Customer customer);
        public Task<bool> Delete(Guid customerId);
        public Task<Customer> FindById(Guid customerId);
        public Task<List<Customer>> FindAll();
    }
}
