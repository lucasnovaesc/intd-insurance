

namespace Service.UseCases.ProductTypeUseCase.Interfaces
{
    public interface IDeleteProductTypeUseCase
    {
        public Task<bool> Delete(Guid productTypeId);
    }
}
