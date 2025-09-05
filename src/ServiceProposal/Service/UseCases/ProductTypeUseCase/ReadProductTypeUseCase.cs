

using Domain.Entities;
using Domain.Factories;
using Domain.Repositories;
using Service.DataTransferObjects.ProductTypeDTO.Response;
using Service.UseCases.ProductTypeUseCase.Interfaces;

namespace Service.UseCases.ProductTypeUseCase
{
    public class ReadProductTypeUseCase : IReadProductTypeUseCase
    {
        private readonly ProductTypeFactory _productTypeFactory;
        private readonly IProductTypeRepository _productTypeRepository;

        public ReadProductTypeUseCase(ProductTypeFactory productTypeFactory, IProductTypeRepository productTypeRepository)
        {
            this._productTypeFactory = productTypeFactory;
            this._productTypeRepository = productTypeRepository;
        }

        public async Task<List<ResponseReadProductTypeDTO>> FindAll()
        {
            try
            {
                List<ProductType> productTypes = await this._productTypeRepository.FindAll();
                List<ResponseReadProductTypeDTO> responseReadProductTypeDTOs = new List<ResponseReadProductTypeDTO>();
                foreach(ProductType productType in productTypes)
                {
                    ResponseReadProductTypeDTO responseReadProductTypeDTO = new ResponseReadProductTypeDTO(
                        productType.ProductTypeId,
                        productType.Name,
                        productType.Description,
                        productType.DateCreation);
                    responseReadProductTypeDTOs.Add(responseReadProductTypeDTO);
                }
                return responseReadProductTypeDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseReadProductTypeDTO> FindById(Guid productTypeId)
        {
            try
            {
                ProductType productType = await this._productTypeRepository.FindById(productTypeId);

                    ResponseReadProductTypeDTO responseReadProductTypeDTO = new ResponseReadProductTypeDTO(
                        productType.ProductTypeId,
                        productType.Name,
                        productType.Description,
                        productType.DateCreation);

                return responseReadProductTypeDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
