

using Domain.Entities;
using Domain.Entities.Enuns;

namespace Domain.Factories
{
    public class ProposalFactory
    {
        public Proposal MakeNew(long proposalNumber, Guid productId, Guid customerId) 
        {
            Guid proposalId = Guid.NewGuid();
            DateTime dateCreation = DateTime.Now;
            DateTime dateModification = DateTime.Now;

            Proposal newProposal = new Proposal(proposalId, proposalNumber, productId, customerId, dateCreation, dateModification, ProposalStatusEnum.Analysing);
            
            return newProposal;

        }
        public Proposal MakeExistent(Guid proposalId, long proposalNumber, Guid productId, Guid customerId, DateTime dateCreation, string proposalStatusId)
        {
            DateTime dateModification = DateTime.Now;
            Enum.TryParse(proposalStatusId, out ProposalStatusEnum proposalStatusEnumId);

            Proposal existentProposal = new Proposal(proposalId, proposalNumber, productId, customerId, dateCreation, dateModification, proposalStatusEnumId);

            return existentProposal;

        }
    }
}
