

namespace Service.DataTransferObjects.ServiceContractingDTO.Request
{
    public class RequestUpdateServiceContractingDTO
    {
        public RequestUpdateServiceContractingDTO()
        {
            
        }

        public RequestUpdateServiceContractingDTO(Guid serviceContractingId, Guid proposalId, Guid customerId, DateTime dateStartContract)
        {
            this.ServiceContractingId = serviceContractingId;
            this.ProposalId = proposalId;
            this.CustomerId = customerId;
            this.DateStartContract = dateStartContract;
        }

        public Guid ServiceContractingId { get; set; }
        public Guid ProposalId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime DateStartContract { get; set; }
    }
}
