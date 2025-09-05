
using Domain.Entities;
using Domain.Factories;
using Domain.Repository;
using Service.DataTransferObjects.ServiceContractingDTO.Request;
using Service.UseCases.ServiceContractingUseCase.Interfaces;

namespace Service.UseCases.ServiceContractingUseCase
{
    public class InsertServiceContractingUseCase : IInsertServiceContractingUseCase
    {
        private readonly IServiceContractingRepository _serviceContractingRepository;
        private readonly ServiceContractingFactory _serviceContractingFactory;

        public InsertServiceContractingUseCase(IServiceContractingRepository serviceContractingRepository, ServiceContractingFactory serviceContractingFactory)
        {
            this._serviceContractingRepository = serviceContractingRepository;
            this._serviceContractingFactory = serviceContractingFactory;
        }

        public async Task<bool> Insert(RequestInsertServiceContractingDTO requestInsertServiceContractingDTO)
        {
            try
            {
                ServiceContracting newServiceContracting = this._serviceContractingFactory.MakeNew(requestInsertServiceContractingDTO.ProposalId, requestInsertServiceContractingDTO.CustomerId);

                bool returnInsertServiceContracting = await this._serviceContractingRepository.Insert(newServiceContracting);
                if(!returnInsertServiceContracting)
                {
                    throw new Exception("It's not possible to insert Service Contracting");
                }
                return returnInsertServiceContracting;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
