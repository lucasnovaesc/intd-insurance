using Domain.Entities;
using Domain.Factories;
using Domain.Repositories;
using Service.DataTransferObjects.ProductDTO.Request;
using Service.UseCases.ProductUseCase.Interfaces;

namespace Service.UseCases.ProductUseCase
{
    public class UpdateProductUseCase : IUpdateProductUseCase
    {
        private readonly ProductFactory _productFactory;
        private readonly IProductRepository _productRepository;

        public UpdateProductUseCase(ProductFactory productFactory, IProductRepository productRepository)
        {
            this._productFactory = productFactory;
            this._productRepository = productRepository;
        }

        public async Task<bool> Update(RequestUpdateProductDTO requestUpdateProductDTO)
        {
            try
            {
                Product existentProduct = this._productFactory.MakeExistent(requestUpdateProductDTO.ProductId, requestUpdateProductDTO.Name, requestUpdateProductDTO.Description,
                                                    requestUpdateProductDTO.ProductTypeId, requestUpdateProductDTO.ProductPrice, requestUpdateProductDTO.DateCreation);
                bool returnUpdateProduct = await this._productRepository.Update(existentProduct);
                if (!returnUpdateProduct)
                {
                    throw new Exception("It's not possible to Update Product");
                }
                return returnUpdateProduct;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
