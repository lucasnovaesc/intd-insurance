

using Domain.Entities;
using Domain.Repositories;
using Service.UseCases.ProductUseCase.Interfaces;

namespace Service.UseCases.ProductUseCase
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductUseCase( IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public async Task<bool> Delete(Guid productId)
        {
            try
            {
                Product existentProduct = await this._productRepository.FindById(productId);
                if(existentProduct == null)
                {
                    throw new Exception("It's not possible to find Product to Delete");
                }

                bool returnDeleteProduct = await this._productRepository.Delete(productId);
                if (!returnDeleteProduct)
                {
                    throw new Exception("It's not possible to Delete Product");
                }

                return returnDeleteProduct;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
