

using Domain.Entities;
using Domain.Factories;
using Domain.Repositories;
using Service.DataTransferObjects.ProductDTO.Request;
using Service.UseCases.ProductUseCase.Interfaces;

namespace Service.UseCases.ProductUseCase
{
    public class InsertProductUseCase : IInsertProductUseCase
    {
        private readonly ProductFactory _productFactory;
        private readonly IProductRepository _productRepository;

        public InsertProductUseCase(ProductFactory productFactory, IProductRepository productRepository)
        {
            this._productFactory = productFactory;
            this._productRepository = productRepository;
        }

        public async Task<bool> Insert(RequestInsertProductDTO requestInsertProductDTO)
        {
            try
            {
                Product newProduct = this._productFactory.MakeNew(requestInsertProductDTO.Name, requestInsertProductDTO.Description, requestInsertProductDTO.ProductTypeId, requestInsertProductDTO.ProductPrice);
                bool returnInsertProduct = await this._productRepository.Insert(newProduct);
                if (!returnInsertProduct)
                {
                    throw new Exception("It's not possible to insert Product");
                }
                return returnInsertProduct;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
