

using Service.DataTransferObjects.ProductTypeDTO.Request;

namespace Service.UseCases.ProductTypeUseCase.Interfaces
{
    public interface IUpdateProductTypeUseCase
    {
        public Task<bool> Update(RequestUpdateProductTypeDTO requestUpdateProductTypeDTO);
    }
}
