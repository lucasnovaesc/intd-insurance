
namespace Service.UseCases.ProductUseCase.Interfaces
{
    public interface IDeleteProductUseCase
    {
        public Task<bool> Delete(Guid productId);
    }
}
