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
            this.ProductId = productId;
            this.Name = name;
            this.Description = description;
            this.ProductTypeId = productTypeId;
            this.Price = price;
            this.DateCreation = dateCreation;
            this.DateModification = dateModification;
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
            get => this._productId; set
            {
                if (value.Equals(Guid.Empty))
                {
                    throw new EntityPropertyIncorrect($"The Product identifier is incorrect: {value}");
                }
                else
                {
                    this._productId = value;
                }
            }
        }
        public string Name
        {
            get => this._name; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new EntityPropertyIncorrect($"The Product name is incorrect: {value}");
                }
                else
                {
                    this._name = value;
                }
            }
        }
        public string Description
        {
            get => this._description; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new EntityPropertyIncorrect($"The Product description is incorrect: {value}");
                }
                else
                {
                    this._description = value;
                }
            }
        }
        public ProductType ProductType { get; set; }
        public Guid ProductTypeId
        {
            get => this._productTypeId; set
            {
                if (value.Equals(Guid.Empty))
                {
                    throw new EntityPropertyIncorrect($"The Product Type identifier is incorrect: {value}");
                }
                else
                {
                    this._productTypeId = value;
                }
            }
        }
        public decimal Price
        {
            get => this._price; set
            {
                if (value < PRICE_MIN_VALUE || value > PRICE_MAX_VALUE)
                {
                    throw new EntityPropertyIncorrect($"The Product price is incorrect: {value}");
                }
                else
                {
                    this._price = value;
                }
            }
        }
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
    }
}
