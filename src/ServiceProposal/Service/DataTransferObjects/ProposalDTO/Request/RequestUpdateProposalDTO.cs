

namespace Service.DataTransferObjects.ProposalDTO.Request
{
    public class RequestUpdateProposalDTO
    {
        public RequestUpdateProposalDTO()
        {
            
        }

        public RequestUpdateProposalDTO(Guid proposalId, long proposalNumber, Guid productId, Guid customerId, string prosposalStatus, DateTime dateCreation)
        {
            this.ProposalId = proposalId;
            this.ProposalNumber = proposalNumber;
            this.ProductId = productId;
            this.CustomerId = customerId;
            this.ProsposalStatus = prosposalStatus;
            this.DateCreation = dateCreation;
        }

        public Guid ProposalId { get; set; }
        public long ProposalNumber { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public string ProsposalStatus { get; set; }
        public DateTime DateCreation { get; set; }

    }
}
