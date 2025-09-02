
namespace Service.UseCases.ServiceContractingUseCase.Interfaces
{
    public interface IDeleteServiceContractingUseCase
    {
        public Task<bool> Delete(Guid serviceContractingId);
    }
}
