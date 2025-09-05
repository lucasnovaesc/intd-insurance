

using Domain.Entities;

namespace Domain.Repository
{
    public interface IServiceContractingRepository
    {
        public Task<bool> Insert(ServiceContracting serviceContracting);
        public Task<bool> Update(ServiceContracting serviceContracting);
        public Task<bool> Delete(Guid serviceContractingId);
        public Task<ServiceContracting> FindById(Guid serviceContractingId);
        public Task<ServiceContracting> FindByProposalId(Guid proposalId);
        public Task<List<ServiceContracting>> FindAll();
    }
}
