

namespace Service.DataTransferObjects.ProductDTO.Request
{
    public class RequestInsertProductDTO
    {
        public RequestInsertProductDTO()
        {
            
        }

        public RequestInsertProductDTO(string name, string description, Guid productTypeId, decimal productPrice)
        {
            this.Name = name;
            this.Description = description;
            this.ProductTypeId = productTypeId;
            this.ProductPrice = productPrice;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProductTypeId { get; set; }
        public decimal ProductPrice { get; set; }

    }
}
