

using Domain.Entities;
using Domain.Factories;
using Domain.Repositories;
using Service.DataTransferObjects.ProductTypeDTO.Request;
using Service.UseCases.ProductTypeUseCase.Interfaces;

namespace Service.UseCases.ProductTypeUseCase
{
    public class UpdateProductTypeUseCase : IUpdateProductTypeUseCase
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly ProductTypeFactory _productTypeFactory;

        public UpdateProductTypeUseCase(IProductTypeRepository productTypeRepository, ProductTypeFactory productTypeFactory)
        {
            this._productTypeRepository = productTypeRepository;
            this._productTypeFactory = productTypeFactory;
        }

        public async Task<bool> Update(RequestUpdateProductTypeDTO requestUpdateProductTypeDTO)
        {
            try
            {
                ProductType existentProductType = this._productTypeFactory.MakeExistent(requestUpdateProductTypeDTO.ProductTypeId, requestUpdateProductTypeDTO.Name, 
                                                                                requestUpdateProductTypeDTO.Description, requestUpdateProductTypeDTO.DateCreation);
                bool returnUpdateProductType = await this._productTypeRepository.Update(existentProductType);
                if (!returnUpdateProductType)
                {
                    throw new Exception("it's not possible to Update Product Type");
                }
                return returnUpdateProductType;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
