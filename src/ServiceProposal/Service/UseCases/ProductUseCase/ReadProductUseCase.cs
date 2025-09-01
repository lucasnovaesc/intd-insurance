

using Domain.Entities;
using Domain.Factories;
using Domain.Repositories;
using Service.DataTransferObjects.ProductDTO.Request;
using Service.DataTransferObjects.ProductDTO.Response;
using Service.UseCases.ProductUseCase.Interfaces;

namespace Service.UseCases.ProductUseCase
{
    public class ReadProductUseCase : IReadProductUseCase
    {
        private readonly ProductFactory _productFactory;
        private readonly IProductRepository _productRepository;

        public ReadProductUseCase(ProductFactory productFactory, IProductRepository productRepository)
        {
            this._productFactory = productFactory;
            this._productRepository = productRepository;
        }

        public async Task<List<ResponseReadProductDTO>> FindAll()
        {
            try
            {
                List<ResponseReadProductDTO> responseReadProductDTOs = new List<ResponseReadProductDTO>();
                List<Product> existentProducts = await this._productRepository.FindAll();
                if (existentProducts == null)
                {
                    throw new Exception("It's not possible to find Product");
                }

                foreach (Product existentProduct in existentProducts)
                {
                    ResponseReadProductDTO responseReadProductDTO = new ResponseReadProductDTO(
                        existentProduct.ProductId,
                        existentProduct.Name,
                        existentProduct.Description,
                        existentProduct.ProductTypeId,
                        existentProduct.Price,
                        existentProduct.DateCreation);
                    responseReadProductDTOs.Add(responseReadProductDTO);

                }

                return responseReadProductDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseReadProductDTO> FindById(Guid productId)
        {
            try
            {
                Product existentProduct = await this._productRepository.FindById(productId);
                if (existentProduct == null)
                {
                    throw new Exception("It's not possible to find Product");
                }

                ResponseReadProductDTO responseReadProductDTO = new ResponseReadProductDTO(
                    existentProduct.ProductId,
                    existentProduct.Name,
                    existentProduct.Description,
                    existentProduct.ProductTypeId,
                    existentProduct.Price,
                    existentProduct.DateCreation);
                return responseReadProductDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ResponseReadProductDTO>> FindByProductTypeId(Guid productTypeId)
        {
            try
            {
                List<ResponseReadProductDTO> responseReadProductDTOs = new List<ResponseReadProductDTO>();
                List<Product> existentProducts = await this._productRepository.FindByProductType(productTypeId);
                if (existentProducts == null)
                {
                    throw new Exception("It's not possible to find Product");
                }

                foreach(Product existentProduct in existentProducts)
                {
                    ResponseReadProductDTO responseReadProductDTO = new ResponseReadProductDTO(
                        existentProduct.ProductId,
                        existentProduct.Name,
                        existentProduct.Description,
                        existentProduct.ProductTypeId,
                        existentProduct.Price,
                        existentProduct.DateCreation);
                    responseReadProductDTOs.Add(responseReadProductDTO);

                }
                
                return responseReadProductDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
