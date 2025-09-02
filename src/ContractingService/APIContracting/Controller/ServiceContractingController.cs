using Microsoft.AspNetCore.Mvc;
using Service.DataTransferObjects.ServiceContractingDTO.Request;
using Service.DataTransferObjects.ServiceContractingDTO.Response;
using Service.UseCases.ServiceContractingUseCase.Interfaces;

namespace APIContracting.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceContractingController : ControllerBase
    {
        private readonly IInsertServiceContractingUseCase _insertServiceContractingUseCase;
        private readonly IUpdateServiceContractingUseCase _updateServiceContractingUseCase;
        private readonly IDeleteServiceContractingUseCase _deleteServiceContractingUseCase;
        private readonly IReadServiceContractingUseCase _readServiceContractingUseCase;

        public ServiceContractingController(IInsertServiceContractingUseCase insertServiceContractingUseCase, IUpdateServiceContractingUseCase updateServiceContractingUseCase,
                                            IDeleteServiceContractingUseCase deleteServiceContractingUseCase, IReadServiceContractingUseCase readServiceContractingUseCase)
        {
            this._insertServiceContractingUseCase = insertServiceContractingUseCase;
            this._updateServiceContractingUseCase = updateServiceContractingUseCase;
            this._deleteServiceContractingUseCase = deleteServiceContractingUseCase;
            this._readServiceContractingUseCase = readServiceContractingUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseReadServiceContractingDTO>>> FindAllServiceContracting()
        {
            try
            {
                List<ResponseReadServiceContractingDTO> returnReadServiceContractingList = await this._readServiceContractingUseCase.FindAll();

                if (returnReadServiceContractingList == null)
                {
                    return BadRequest("Failed to Find Service Contracting.");
                }

                return Ok(returnReadServiceContractingList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{proposalId}", Name = "FindServiceContractingByProposalId")]
        public async Task<ActionResult<ResponseReadServiceContractingDTO>> FindServiceContractingByProposalId(Guid proposalId)
        {
            try
            {
                ResponseReadServiceContractingDTO returnReadServiceContracting = await this._readServiceContractingUseCase.FindByProposalId(proposalId);

                if (returnReadServiceContracting == null)
                {
                    return BadRequest("Failed to Find Service Contracting.");
                }

                return Ok(returnReadServiceContracting);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{serviceContractingId}", Name = "FindServiceContractingById")]
        public async Task<ActionResult<ResponseReadServiceContractingDTO>> FindServiceContractingById(Guid serviceContractingId)
        {
            try
            {
                ResponseReadServiceContractingDTO returnReadServiceContracting = await this._readServiceContractingUseCase.FindById(serviceContractingId);

                if (returnReadServiceContracting == null)
                {
                    return BadRequest("Failed to Find Service Contracting.");
                }

                return Ok(returnReadServiceContracting);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> InsertProposal(RequestInsertServiceContractingDTO requestInsertServiceContractingDTO)
        {
            try
            {
                bool returnInsertServiceContracting = await this._insertServiceContractingUseCase.Insert(requestInsertServiceContractingDTO);

                if (returnInsertServiceContracting == null)
                {
                    return BadRequest("Failed to insert Proposal.");
                }

                return Ok(returnInsertServiceContracting);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateProposal(RequestUpdateServiceContractingDTO requestUpdateServiceContractingDTO)
        {
            try
            {
                bool returnUpdateServiceContracting = await this._updateServiceContractingUseCase.Update(requestUpdateServiceContractingDTO);

                if (returnUpdateServiceContracting == null)
                {
                    return BadRequest("Failed to update Proposal.");
                }

                return Ok(returnUpdateServiceContracting);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{serviceContractingId}")]
        public async Task<ActionResult<bool>> DeleteProposal(Guid serviceContractingId)
        {
            try
            {
                bool returnDeleteServiceContracting = await this._deleteServiceContractingUseCase.Delete(serviceContractingId);

                if (returnDeleteServiceContracting == null)
                {
                    return BadRequest("Failed to delete Proposal.");
                }

                return Ok(returnDeleteServiceContracting);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
