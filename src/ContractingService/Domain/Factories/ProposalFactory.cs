

using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Factories
{
    public class ProposalFactory
    {
        public Proposal MakeNew(long proposalNumber, Guid productId, Guid customerId)
        {
            Guid proposalId = Guid.NewGuid();
            DateTime dateCreation = DateTime.Now;
            DateTime dateModification = DateTime.Now;

            Proposal newProposal = new Proposal(proposalId, proposalNumber, productId, customerId, dateCreation, dateModification);

            return newProposal;

        }
        public Proposal MakeExistent(Guid proposalId, long proposalNumber, Guid productId, Guid customerId, DateTime dateCreation)
        {
            DateTime dateModification = DateTime.Now;

            Proposal existentProposal = new Proposal(proposalId, proposalNumber, productId, customerId, dateCreation, dateModification);

            return existentProposal;

        }
    }
}
