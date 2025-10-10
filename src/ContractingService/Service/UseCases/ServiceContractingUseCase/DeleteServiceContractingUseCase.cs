using Domain.Entities;
using Domain.Factories;
using Domain.Repository;
using Service.UseCases.ServiceContractingUseCase.Interfaces;

namespace Service.UseCases.ServiceContractingUseCase
{
    public class DeleteServiceContractingUseCase : IDeleteServiceContractingUseCase
    {
        private readonly IServiceContractingRepository _serviceContractingRepository;
        private readonly ServiceContractingFactory _serviceContractingFactory;

        public DeleteServiceContractingUseCase(IServiceContractingRepository serviceContractingRepository, ServiceContractingFactory serviceContractingFactory)
        {
            this._serviceContractingRepository = serviceContractingRepository;
            this._serviceContractingFactory = serviceContractingFactory;
        }

        public async Task<bool> Delete(Guid serviceContractingId)
        {
            try
            {
                ServiceContracting existentServiceContracting = await this._serviceContractingRepository.FindById(serviceContractingId);
                if (existentServiceContracting == null)
                {
                    throw new Exception("It's not possible to find Service Contracting");
                }
                bool returnDeleteServiceContracting = await this._serviceContractingRepository.Delete(serviceContractingId);
                if (!returnDeleteServiceContracting)
                {
                    throw new Exception("it's not possible to delete Service Contracting");
                }
                return returnDeleteServiceContracting;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
