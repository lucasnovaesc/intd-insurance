

namespace Service.DataTransferObjects.ProductDTO.Request
{
    public class RequestUpdateProductDTO
    {
        public RequestUpdateProductDTO()
        {
            
        }

        public RequestUpdateProductDTO(Guid productId, string name, string description, decimal productPrice, Guid productTypeId, DateTime dateCreation)
        {
            this.ProductId = productId;
            this.Name = name;
            this.Description = description;
            this.ProductPrice = productPrice;
            this.ProductTypeId = productTypeId;
            this.DateCreation = dateCreation;
        }

        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal ProductPrice { get; set; }
        public Guid ProductTypeId { get; set; }
        public DateTime DateCreation { get; set; }

    }
}
