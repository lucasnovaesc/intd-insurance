

using Service.DataTransferObjects.ProductTypeDTO.Response;

namespace Service.UseCases.ProductTypeUseCase.Interfaces
{
    public interface IReadProductTypeUseCase
    {
        public Task<ResponseReadProductTypeDTO> FindById(Guid productTypeId);
        public Task<List<ResponseReadProductTypeDTO>> FindAll();
    }
}
