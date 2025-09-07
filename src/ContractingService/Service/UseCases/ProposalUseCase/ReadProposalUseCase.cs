

using Domain.Entities;
using Domain.Factories;
using Domain.Repository;
using Service.DataTransferObjects.ProposalDTO.Response;
using Service.UseCases.ProposalUseCase.Interfaces;

namespace Service.UseCases.ProposalUseCase
{
    public class ReadProposalUseCase : IReadProposalUseCase
    {
        private readonly ProposalFactory _proposalFactory;
        private readonly IProposalRepository _proposalRepository;

        public ReadProposalUseCase(ProposalFactory proposalFactory, IProposalRepository proposalRepository)
        {
            this._proposalFactory = proposalFactory;
            this._proposalRepository = proposalRepository;
        }

        public async Task<List<ResponseReadProposalDTO>> FindAll()
        {
            try
            {
                List<ResponseReadProposalDTO> responseReadProposalDTOs = new List<ResponseReadProposalDTO>();
                List<Proposal> proposals = await this._proposalRepository.FindAll();
                foreach (Proposal proposal in proposals)
                {
                    ResponseReadProposalDTO responseReadProposalDTO = new ResponseReadProposalDTO(
                        proposal.ProposalId,
                        proposal.ProposalNumber,
                        proposal.ProductId,
                        proposal.CustomerId,
                        proposal.DateCreation);
                    responseReadProposalDTOs.Add(responseReadProposalDTO);
                }
                return responseReadProposalDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ResponseReadProposalDTO>> FindByCustomerId(Guid customerId)
        {
            try
            {
                List<ResponseReadProposalDTO> responseReadProposalDTOs = new List<ResponseReadProposalDTO>();
                List<Proposal> proposals = await this._proposalRepository.FindByCustomerId(customerId);
                foreach (Proposal proposal in proposals)
                {
                    ResponseReadProposalDTO responseReadProposalDTO = new ResponseReadProposalDTO(
                        proposal.ProposalId,
                        proposal.ProposalNumber,
                        proposal.ProductId,
                        proposal.CustomerId,
                        proposal.DateCreation);
                    responseReadProposalDTOs.Add(responseReadProposalDTO);
                }
                return responseReadProposalDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseReadProposalDTO> FindById(Guid proposalId)
        {
            try
            {
                Proposal proposal = await this._proposalRepository.FindById(proposalId);

                ResponseReadProposalDTO responseReadProposalDTO = new ResponseReadProposalDTO(
                    proposal.ProposalId,
                    proposal.ProposalNumber,
                    proposal.ProductId,
                    proposal.CustomerId,
                    proposal.DateCreation);

                return responseReadProposalDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseReadProposalDTO> FindByProposalNumber(long proposalNumber)
        {
            try
            {
                Proposal proposal = await this._proposalRepository.FindByProposalNumber(proposalNumber);

                ResponseReadProposalDTO responseReadProposalDTO = new ResponseReadProposalDTO(
                    proposal.ProposalId,
                    proposal.ProposalNumber,
                    proposal.ProductId,
                    proposal.CustomerId,
                    proposal.DateCreation);

                return responseReadProposalDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
