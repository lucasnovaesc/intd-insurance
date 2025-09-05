


using Domain.Entities;

namespace Domain.Factories
{
    public class ServiceContractingFactory
    {
        public ServiceContracting MakeNew(Guid proposalId, Guid customerId)
        {
            Guid serviceContractingId = Guid.NewGuid();
            DateTime dateStartContract = DateTime.Now;
            return new ServiceContracting(serviceContractingId, proposalId, customerId, dateStartContract);
        }

        public ServiceContracting MakeExistent(Guid serviceContractingId, Guid proposalId, Guid customerId, DateTime dateStartContract)
        {
            return new ServiceContracting(serviceContractingId, proposalId, customerId, dateStartContract);
        }
    }
}
