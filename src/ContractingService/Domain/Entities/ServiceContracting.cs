

using Domain.Exceptions;

namespace Domain.Entities
{
    public class ServiceContracting
    {
        public ServiceContracting()
        {
            
        }

        public ServiceContracting(Guid serviceContractingId, Guid proposalId, Guid customerId, DateTime dateStartContract)
        {
            this.ServiceContractingId = serviceContractingId;
            this.ProposalId = proposalId;
            this.CustomerId = customerId;
            this.DateStartContract = dateStartContract;
        }

        private Guid _serviceContractingId;
        private Guid _proposalId;
        private Guid _customerId;

        public Guid ServiceContractingId
        {
            get => _serviceContractingId; set
            {
                if (value.Equals(Guid.Empty))
                {
                    throw new EntityPropertyIncorrect($"The Service Contracting identifier is incorrect: {value}");
                }
                else
                {
                    this._serviceContractingId = value;
                }
            }
        }
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
        public DateTime DateStartContract { get; set; }



    }
}
