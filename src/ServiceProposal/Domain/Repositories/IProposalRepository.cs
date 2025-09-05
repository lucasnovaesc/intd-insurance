

using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProposalRepository
    {
        public Task<bool> Insert(Proposal proposal);
        public Task<bool> Update(Proposal proposal);
        public Task<bool> Delete(Guid proposalId);
        public Task<Proposal> FindById(Guid proposalId);
        public Task<List<Proposal>> FindAll();
        public Task<List<Proposal>> FindByCustomerId(Guid customerId);
        public Task<Proposal> FindByProposalNumber(long proposalNumber);
    }
}
