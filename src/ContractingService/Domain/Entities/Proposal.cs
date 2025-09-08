using Domain.Exceptions;

namespace Domain.Entities
{
    public class Proposal
    {
        private Proposal()
        {
            
        }

        public Proposal(Guid proposalId, long proposalNumber, Guid productId, Guid customerId, DateTime dateCreation, DateTime dateModification)
        {
            this.ProposalId = proposalId;
            this.ProposalNumber = proposalNumber;
            this.ProductId = productId;
            this.CustomerId = customerId;
            this.DateCreation = dateCreation;
            this.DateModification = dateModification;
        }

        private Guid _proposalId;
        private long _proposalNumber;
        private Guid _customerId;
        private Guid _productId;


        public Guid ProposalId
        {
            get => _proposalId; set
            {
                if (value.Equals(Guid.Empty))
                {
                    throw new EntityPropertyIncorrect($"The Proposal identifier is incorrect: {value}");
                }
                else
                {
                    this._proposalId = value;
                }
            }
        }
        public long ProposalNumber { get; set; }

        public Guid ProductId
        {
            get => _productId; set
            {
                if (value.Equals(Guid.Empty))
                {
                    throw new EntityPropertyIncorrect($"The Product identifier is incorrect: {value}");
                }
                else
                {
                    this._productId = value;
                }
            }
        }
        public Guid CustomerId
        {
            get => _customerId; set
            {
                if (value.Equals(Guid.Empty))
                {
                    throw new EntityPropertyIncorrect($"The Customer identifier is incorrect: {value}");
                }
                else
                {
                    this._customerId = value;
                }
            }
        }
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
    }
}
