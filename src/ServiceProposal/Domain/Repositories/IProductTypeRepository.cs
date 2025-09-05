

using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProductTypeRepository
    {
        public Task<bool> Insert(ProductType productType);
        public Task<bool> Update(ProductType productType);
        public Task<bool> Delete(Guid productTypeId);
        public Task<ProductType> FindById(Guid productTypeId);
        public Task<List<ProductType>> FindAll();
    }
}
