

using Domain.Entities.Enuns;

namespace Domain.Entities
{
    public class ProposalStatus
    {
        public ProposalStatus(ProposalStatusEnum statusSystemId, string description, DateTime dateCreation, DateTime dateModification)
        {
            this.StatusSystemId = statusSystemId;
            this.Description = description;
            this.DateCreation = dateCreation;
            this.DateModification = dateModification;
        }

        public ProposalStatusEnum StatusSystemId { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
        public static int minValue = 0;
        public static int maxValue = Enum.GetValues(typeof(ProposalStatusEnum)).Length;
    }
}
