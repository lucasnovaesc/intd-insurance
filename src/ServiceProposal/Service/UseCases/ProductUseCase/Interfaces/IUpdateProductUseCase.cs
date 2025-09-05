
using Service.DataTransferObjects.ProductDTO.Request;

namespace Service.UseCases.ProductUseCase.Interfaces
{
    public interface IUpdateProductUseCase
    {
        public Task<bool> Update(RequestUpdateProductDTO requestUpdateProductDTO);
    }
}
