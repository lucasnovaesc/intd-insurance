

using Domain.Entities;
using Domain.Repositories;
using Service.UseCases.ProductTypeUseCase.Interfaces;

namespace Service.UseCases.ProductTypeUseCase
{
    public class DeleteProductTypeUseCase : IDeleteProductTypeUseCase
    {
        private readonly IProductTypeRepository _productTypeRepository;

        public DeleteProductTypeUseCase(IProductTypeRepository productTypeRepository)
        {
            this._productTypeRepository = productTypeRepository;
        }

        public async Task<bool> Delete(Guid productTypeId)
        {
            try
            {
                ProductType existentProductType = await this._productTypeRepository.FindById(productTypeId);
                if (existentProductType == null)
                {
                    throw new Exception("It's not possible to find Product Type");
                }
                bool returnDeleteProductType = await this._productTypeRepository.Delete(productTypeId);
                if(!returnDeleteProductType)
                {
                    throw new Exception("it's not possible to delete Product Type");
                }
                return returnDeleteProductType;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
