

using Domain.Entities;

namespace Domain.Repository
{
    public interface IProposalRepository
    {
        public Task<bool> Insert(Proposal proposal);
        public Task<Proposal> FindById(Guid proposalId);
        public Task<List<Proposal>> FindAll();
        public Task<List<Proposal>> FindByCustomerId(Guid customerId);
        public Task<Proposal> FindByProposalNumber(long proposalNumber);
    }
}
