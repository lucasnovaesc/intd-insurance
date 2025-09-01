

using Service.DataTransferObjects.ProductDTO.Response;

namespace Service.UseCases.ProductUseCase.Interfaces
{
    public interface IReadProductUseCase
    {
        public Task<ResponseReadProductDTO> FindById(Guid productId);
        public Task<List<ResponseReadProductDTO>> FindByProductTypeId(Guid productTypeId);
        public Task<List<ResponseReadProductDTO>> FindAll();

    }
}
