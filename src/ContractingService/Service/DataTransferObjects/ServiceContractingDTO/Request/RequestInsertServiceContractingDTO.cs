

namespace Service.DataTransferObjects.ServiceContractingDTO.Request
{
    public class RequestInsertServiceContractingDTO
    {
        public RequestInsertServiceContractingDTO()
        {
            
        }

        public RequestInsertServiceContractingDTO(Guid proposalId, Guid customerId)
        {
            this.ProposalId = proposalId;
            this.CustomerId = customerId;
        }

        public Guid ProposalId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
