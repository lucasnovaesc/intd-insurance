

using Domain.Exceptions;
using System.Xml.Linq;

namespace Domain.Entities
{
    public class ProductType
    {
        private ProductType()
        {
            
        }
        public ProductType(Guid productTypeId, string name, string description, DateTime dateCreation, DateTime dateModification)
        {
            this.ProductTypeId = productTypeId;
            this.Name = name;
            this.Description = description;
            this.DateCreation = dateCreation;
            this.DateModification = dateModification;
        }
        private Guid _productTypeId;
        private string _name;
        private string _description;

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
                    this._productTypeId = value;
                }
            }
        }
        public string Name
        {
            get => _name; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new EntityPropertyIncorrect($"The Product Type name is incorrect: {value}");
                }
                else
                {
                    this._name = value;
                }
            }
        }
        public string Description
        {
            get => _description; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new EntityPropertyIncorrect($"The Product Type description is incorrect: {value}");
                }
                else
                {
                    this._description = value;
                }
            }
        }
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
    }
}
