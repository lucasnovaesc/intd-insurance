
using Domain.Entities;
using Domain.Factories;
using Domain.Repository;
using Service.DataTransferObjects.ServiceContractingDTO.Response;
using Service.UseCases.ServiceContractingUseCase.Interfaces;
using System.Runtime.Serialization;

namespace Service.UseCases.ServiceContractingUseCase
{
    public class ReadServiceContractingUseCase : IReadServiceContractingUseCase
    {
        private readonly IServiceContractingRepository _serviceContractingRepository;
        private readonly ServiceContractingFactory _serviceContractingFactory;

        public ReadServiceContractingUseCase(IServiceContractingRepository serviceContractingRepository, ServiceContractingFactory serviceContractingFactory)
        {
            this._serviceContractingRepository = serviceContractingRepository;
            this._serviceContractingFactory = serviceContractingFactory;
        }

        public async Task<List<ResponseReadServiceContractingDTO>> FindAll()
        {
            try
            {
                List<ResponseReadServiceContractingDTO> responseReadServiceContractings = new List<ResponseReadServiceContractingDTO>();
                List<ServiceContracting> serviceContractings = await this._serviceContractingRepository.FindAll();
                foreach(ServiceContracting serviceContracting in serviceContractings)
                {
                    ResponseReadServiceContractingDTO responseReadServiceContracting = new ResponseReadServiceContractingDTO(
                        serviceContracting.ServiceContractingId,
                        serviceContracting.ProposalId,
                        serviceContracting.CustomerId,
                        serviceContracting.DateStartContract);
                    responseReadServiceContractings.Add(responseReadServiceContracting);
                }
                return responseReadServiceContractings;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseReadServiceContractingDTO> FindById(Guid serviceContractingId)
        {
            try
            {
                ServiceContracting serviceContracting = await this._serviceContractingRepository.FindById(serviceContractingId);
                ResponseReadServiceContractingDTO responseReadServiceContracting = new ResponseReadServiceContractingDTO(
                    serviceContracting.ServiceContractingId,
                    serviceContracting.ProposalId,
                    serviceContracting.CustomerId,
                    serviceContracting.DateStartContract);
                return responseReadServiceContracting;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseReadServiceContractingDTO> FindByProposalId(Guid proposalId)
        {
            try
            {
                ServiceContracting serviceContracting = await this._serviceContractingRepository.FindByProposalId(proposalId);
                ResponseReadServiceContractingDTO responseReadServiceContracting = new ResponseReadServiceContractingDTO(
                    serviceContracting.ServiceContractingId,
                    serviceContracting.ProposalId,
                    serviceContracting.CustomerId,
                    serviceContracting.DateStartContract);
                return responseReadServiceContracting;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
