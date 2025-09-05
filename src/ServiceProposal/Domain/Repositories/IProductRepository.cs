

using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        public Task<bool> Insert(Product product);
        public Task<bool> Update(Product product);
        public Task<bool> Delete(Guid productId);
        public Task<Product> FindById(Guid productId);
        public Task<List<Product>> FindByProductType(Guid productTypeId);
        public Task<List<Product>> FindAll();
    }
}
