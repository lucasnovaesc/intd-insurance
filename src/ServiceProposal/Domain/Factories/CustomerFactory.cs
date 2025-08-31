

using Domain.Entities;

namespace Domain.Factories
{
    public class CustomerFactory
    {
        public Customer MakeNew(string name, string email, string phoneNumber, string address)
        {
            Guid customerId = Guid.NewGuid();
            DateTime dateCreation = DateTime.Now;
            DateTime dateModification = DateTime.Now;

            Customer newCustomer = new Customer(customerId, name, email, phoneNumber, address, dateCreation, dateModification);

            return newCustomer;
        }

        public  Customer MakeExistent(Guid customerId, string name, string email, string phoneNumber, string address, DateTime dateCreation)
        {
            DateTime dateModification = DateTime.Now;

            Customer existentCustomer = new Customer(customerId, name, email, phoneNumber, address, dateCreation, dateModification);

            return existentCustomer;
        }
    }
}
