

using System.Text.Json.Serialization;

namespace Infrastructure.Resources.RabbitMq
{
    public class ProposalDTO
    {
        [JsonConstructor]
        public ProposalDTO(string proposalId, int proposalNumber, string product, string productId, string customer, string customerId, DateTime dateCreation, DateTime dateModification, string proposalStatus, int proposalStatusId)
        {
            ProposalId = proposalId;
            ProposalNumber = proposalNumber;
            Product = product;
            ProductId = productId;
            Customer = customer;
            CustomerId = customerId;
            ProposalStatus = proposalStatus;
            ProposalStatusId = proposalStatusId;
            DateCreation = dateCreation;
            DateModification = dateModification;
        }

        // propriedades
        public string ProposalId { get; set; }
        public int ProposalNumber { get; set; }
        public string Product { get; set; }
        public string ProductId { get; set; }
        public string Customer { get; set; }
        public string CustomerId { get; set; }
        public string ProposalStatus { get; set; }
        public int ProposalStatusId { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
    }
}
