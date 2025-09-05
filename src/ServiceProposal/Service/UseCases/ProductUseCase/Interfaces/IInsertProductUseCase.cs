
using Service.DataTransferObjects.ProductDTO.Request;

namespace Service.UseCases.ProductUseCase.Interfaces
{
    public interface IInsertProductUseCase
    {
        public Task<bool> Insert(RequestInsertProductDTO requestInsertProductDTO);
    }
}
