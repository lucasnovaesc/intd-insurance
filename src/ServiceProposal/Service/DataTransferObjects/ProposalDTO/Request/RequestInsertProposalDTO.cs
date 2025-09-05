

namespace Service.DataTransferObjects.ProposalDTO.Request
{
    public class RequestInsertProposalDTO
    {
        public RequestInsertProposalDTO()
        {
            
        }

        public RequestInsertProposalDTO(long proposalNumber, Guid productId, Guid customerId, string prosposalStatus)
        {
            this.ProposalNumber = proposalNumber;
            this.ProductId = productId;
            this.CustomerId = customerId;
            this.ProsposalStatus = prosposalStatus;
        }

        public long ProposalNumber { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public string ProsposalStatus { get; set; }
    }
}
