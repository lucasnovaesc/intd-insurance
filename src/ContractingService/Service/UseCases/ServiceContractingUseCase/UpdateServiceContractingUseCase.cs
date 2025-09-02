
using Domain.Entities;
using Domain.Factories;
using Domain.Repository;
using Service.DataTransferObjects.ServiceContractingDTO.Request;
using Service.UseCases.ServiceContractingUseCase.Interfaces;

namespace Service.UseCases.ServiceContractingUseCase
{
    public class UpdateServiceContractingUseCase : IUpdateServiceContractingUseCase
    {
        private readonly IServiceContractingRepository _serviceContractingRepository;
        private readonly ServiceContractingFactory _serviceContractingFactory;

        public UpdateServiceContractingUseCase(IServiceContractingRepository serviceContractingRepository, ServiceContractingFactory serviceContractingFactory)
        {
            this._serviceContractingRepository = serviceContractingRepository;
            this._serviceContractingFactory = serviceContractingFactory;
        }

        public async Task<bool> Update(RequestUpdateServiceContractingDTO requestUpdateServiceContractingDTO)
        {
            try
            {
                ServiceContracting existentServiceContracting = this._serviceContractingFactory.MakeExistent(requestUpdateServiceContractingDTO.ServiceContractingId, requestUpdateServiceContractingDTO.ProposalId,
                                                                                                            requestUpdateServiceContractingDTO.CustomerId, requestUpdateServiceContractingDTO.DateStartContract);

                bool returnUpdateServiceContracting = await this._serviceContractingRepository.Update(existentServiceContracting);
                if (!returnUpdateServiceContracting)
                {
                    throw new Exception("It's not possible to Update Service Contracting");
                }
                return returnUpdateServiceContracting;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
