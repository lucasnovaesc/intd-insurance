using Domain.Exceptions;

namespace Domain.Entities
{
    public class Product
    {
        private Product()
        {
            
        }
        public Product(Guid productId, string name, string description, Guid productTypeId, decimal price, DateTime dateCreation, DateTime dateModification)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            ProductTypeId = productTypeId;
            Price = price;
            DateCreation = dateCreation;
            DateModification = dateModification;
        }

        private Guid _productId;
        private string _name;
        private string _description;
        private Guid _productTypeId;
        private decimal _price;
        private const decimal PRICE_MIN_VALUE = 0.01M;
        private const decimal PRICE_MAX_VALUE = 1000000.00M;
        
        public Guid ProductId
        {
            get => _productId; set
            {
                if (value.Equals(Guid.Empty))
                {
                    throw new EntityPropertyIncorrect($"The Product identifier is incorrect: {value}");
                }
                else
                {
                    _productId = value;
                }
            }
        }
        public string Name
        {
            get => _name; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new EntityPropertyIncorrect($"The Product name is incorrect: {value}");
                }
                else
                {
                    _name = value;
                }
            }
        }
        public string Description
        {
            get => _description; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new EntityPropertyIncorrect($"The Product description is incorrect: {value}");
                }
                else
                {
                    _description = value;
                }
            }
        }
        public ProductType ProductType { get; set; }
        public Guid ProductTypeId
        {
            get => _productTypeId; set
            {
                if (value.Equals(Guid.Empty))
                {
                    throw new EntityPropertyIncorrect($"The Product Type identifier is incorrect: {value}");
                }
                else
                {
                    _productTypeId = value;
                }
            }
        }
        public decimal Price
        {
            get => _price; set
            {
                if (value < PRICE_MIN_VALUE || value > PRICE_MAX_VALUE)
                {
                    throw new EntityPropertyIncorrect($"The Product price is incorrect: {value}");
                }
                else
                {
                    _price = value;
                }
            }
        }
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
    }
}
