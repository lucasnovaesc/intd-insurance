
using Domain.Entities;
using Domain.Factories;
using Domain.Repositories;
using Service.DataTransferObjects.ProductTypeDTO.Request;
using Service.UseCases.ProductTypeUseCase.Interfaces;

namespace Service.UseCases.ProductTypeUseCase
{
    public class InsertProductTypeUseCase : IInsertProductTypeUseCase
    {

        private readonly IProductTypeRepository _productTypeRepository;
        private readonly ProductTypeFactory _productTypeFactory;

        public InsertProductTypeUseCase(IProductTypeRepository productTypeRepository, ProductTypeFactory productTypeFactory)
        {
            this._productTypeRepository = productTypeRepository;
            this._productTypeFactory = productTypeFactory;
        }

        public async Task<bool> Insert(RequestInsertProductTypeDTO requestInsertrProductTypeDTO)
        {
            try
            {
                ProductType newProductType = this._productTypeFactory.MakeNew(requestInsertrProductTypeDTO.Name, requestInsertrProductTypeDTO.Description);
                bool returnInsertProductType = await this._productTypeRepository.Insert(newProductType);
                if (!returnInsertProductType)
                {
                    throw new Exception("it's not possible to insert Product Type");
                }
                return returnInsertProductType;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
