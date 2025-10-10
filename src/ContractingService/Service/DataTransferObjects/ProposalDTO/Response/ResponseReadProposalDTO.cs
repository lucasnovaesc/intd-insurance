

namespace Service.DataTransferObjects.ProposalDTO.Response
{
    public class ResponseReadProposalDTO
    {
        public ResponseReadProposalDTO()
        {

        }

        public ResponseReadProposalDTO(Guid proposalId, long proposalNumber, Guid productId, Guid customerId, DateTime dateCreation)
        {
            this.ProposalId = proposalId;
            this.ProposalNumber = proposalNumber;
            this.ProductId = productId;
            this.CustomerId = customerId;
            this.DateCreation = dateCreation;
        }

        public Guid ProposalId { get; set; }
        public long ProposalNumber { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime DateCreation { get; set; }

    }
}
