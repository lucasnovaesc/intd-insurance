

namespace Service.DataTransferObjects.ProductDTO.Response
{
    public class ResponseReadProductDTO
    {
        public ResponseReadProductDTO()
        {
            
        }

        public ResponseReadProductDTO(Guid productId, string name, string description, Guid productTypeId, decimal productPrice, DateTime dateCreation)
        {
            this.ProductId = productId;
            this.Name = name;
            this.Description = description;
            this.ProductTypeId = productTypeId;
            this.ProductPrice = productPrice;
            this.DateCreation = dateCreation;
        }

        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProductTypeId { get; set; }
        public decimal ProductPrice { get; set; }
        public DateTime DateCreation { get; set; }
    }
}
