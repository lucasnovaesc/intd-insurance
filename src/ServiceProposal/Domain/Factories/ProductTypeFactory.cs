

using Domain.Entities;

namespace Domain.Factories
{
    public class ProductTypeFactory
    {
        public ProductType MakeNew(string name, string description)
        {
            Guid productTypeId = Guid.NewGuid();
            DateTime dateCreation = DateTime.Now;
            DateTime dateModification = DateTime.Now;

            ProductType newProductType = new ProductType(productTypeId, name, description, dateCreation, dateModification);

            return newProductType;
        }
        public ProductType MakeExistent(Guid productTypeId, string name, string description, DateTime dateCreation)
        {
            DateTime dateModification = DateTime.Now;

            ProductType existentProductType = new ProductType(productTypeId, name, description, dateCreation, dateModification);

            return existentProductType;
        }
    }
}
