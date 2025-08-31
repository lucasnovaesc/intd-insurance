using Domain.Entities;

namespace Domain.Factories
{
    public class ProductFactory
    {
        public Product MakeNew(string name, string description, Guid productTypeId, decimal price)
        {
            Guid productId = Guid.NewGuid();
            DateTime dateCreation = DateTime.Now;
            DateTime dateModification = DateTime.Now;

            Product newProduct = new Product(productId, name, description, productTypeId, price, dateCreation, dateModification);

            return newProduct;
        }
        public Product MakeExistent(Guid productId, string name, string description, Guid productTypeId, decimal price, DateTime dateCreation)
        {
            DateTime dateModification = DateTime.Now;

            Product existentProduct = new Product(productId, name, description, productTypeId, price, dateCreation, dateModification);

            return existentProduct;
        }
    }
}
