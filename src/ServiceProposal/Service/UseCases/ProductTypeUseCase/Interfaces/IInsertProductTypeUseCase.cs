
using Service.DataTransferObjects.ProductTypeDTO.Request;

namespace Service.UseCases.ProductTypeUseCase.Interfaces
{
    public interface IInsertProductTypeUseCase
    {
        public Task<bool> Insert(RequestInsertProductTypeDTO requestInsertrProductTypeDTO);
    }
}
