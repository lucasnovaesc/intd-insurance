

using Domain.Entities;
using Domain.Entities.Enuns;
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

            Proposal newProposal = new Proposal(proposalId, proposalNumber, productId, customerId, dateCreation, dateModification, ProposalStatusEnum.Analysing);
            
            return newProposal;

        }
        public Proposal MakeExistent(Guid proposalId, long proposalNumber, Guid productId, Guid customerId, DateTime dateCreation, string proposalStatusId)
        {
            DateTime dateModification = DateTime.Now;
            if (!Enum.TryParse(proposalStatusId, true, out ProposalStatusEnum parsedStatus))
                throw new EntityPropertyIncorrect($"Invalid ProposalStatus: {proposalStatusId}");

            Proposal existentProposal = new Proposal(proposalId, proposalNumber, productId, customerId, dateCreation, dateModification, parsedStatus);

            return existentProposal;

        }
    }
}
