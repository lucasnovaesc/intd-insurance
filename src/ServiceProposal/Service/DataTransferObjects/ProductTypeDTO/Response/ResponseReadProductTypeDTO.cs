

namespace Service.DataTransferObjects.ProductTypeDTO.Response
{
    public class ResponseReadProductTypeDTO
    {
        public ResponseReadProductTypeDTO()
        {
            
        }

        public ResponseReadProductTypeDTO(Guid productTypeId, string name, string description, DateTime dateCreation)
        {
            this.ProductTypeId = productTypeId;
            this.Name = name;
            this.Description = description;
            this.DateCreation = dateCreation;
        }

        public Guid ProductTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }

    }
}
