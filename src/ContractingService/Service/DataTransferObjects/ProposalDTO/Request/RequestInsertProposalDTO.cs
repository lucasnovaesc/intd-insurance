

namespace Service.DataTransferObjects.ProposalDTO.Request
{
    public class RequestInsertProposalDTO
    {
        public RequestInsertProposalDTO()
        {

        }

        public RequestInsertProposalDTO(Guid proposalId, long proposalNumber, Guid productId, Guid customerId, DateTime dateCreation, DateTime dateModification)
        {
            this.ProposalId = proposalId;
            this.ProposalNumber = proposalNumber;
            this.ProductId = productId;
            this.CustomerId = customerId;
            this.DateCreation = dateCreation;
            this.DateModification = dateModification;
        }

        public Guid ProposalId { get; set; }
        public long ProposalNumber { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
    }
}
