using Microsoft.AspNetCore.Mvc;
using Service.DataTransferObjects.ProposalDTO.Request;
using Service.DataTransferObjects.ProposalDTO.Response;
using Service.UseCases.ProposalUseCase.Interfaces;

namespace API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProposalController : ControllerBase
    {
        private readonly IInsertProposalUseCase _insertProposalUseCase;
        private readonly IUpdateProposalUseCase _updateProposalUseCase;
        private readonly IDeleteProposalUseCase _deleteProposalUseCase;
        private readonly IReadProposalUseCase _readProposalUseCase;

        public ProposalController(IInsertProposalUseCase insertProposalUseCase, IUpdateProposalUseCase updateProposalUseCase, IDeleteProposalUseCase deleteProposalUseCase, IReadProposalUseCase readProposalUseCase)
        {
            this._insertProposalUseCase = insertProposalUseCase;
            this._updateProposalUseCase = updateProposalUseCase;
            this._deleteProposalUseCase = deleteProposalUseCase;
            this._readProposalUseCase = readProposalUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseReadProposalDTO>>> FindAllProposal()
        {
            try
            {
                List<ResponseReadProposalDTO> returnReadProposalList = await this._readProposalUseCase.FindAll();

                if (returnReadProposalList == null)
                {
                    return BadRequest("Failed to Find proposal.");
                }

                return Ok(returnReadProposalList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{customerId}", Name = "FindAllProposalByCustomerId")]
        public async Task<ActionResult<IEnumerable<ResponseReadProposalDTO>>> FindAllProposalByCustomerId(Guid customerId)
        {
            try
            {
                List<ResponseReadProposalDTO> returnReadProposalList = await this._readProposalUseCase.FindByCustomerId(customerId);

                if (returnReadProposalList == null)
                {
                    return BadRequest("Failed to Find proposal.");
                }

                return Ok(returnReadProposalList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{proposalId}", Name = "FindProposalById")]
        public async Task<ActionResult<ResponseReadProposalDTO>> FindProposalById(Guid proposalId)
        {
            try
            {
                ResponseReadProposalDTO returnReadProposal = await this._readProposalUseCase.FindById(proposalId);

                if (returnReadProposal == null)
                {
                    return BadRequest("Failed to Find proposal.");
                }

                return Ok(returnReadProposal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{proposalNumber}", Name = "FindProposalByProposalNumber")]
        public async Task<ActionResult<ResponseReadProposalDTO>> FindProposalByProposalNumber(long proposalNumber)
        {
            try
            {
                ResponseReadProposalDTO returnReadProposal = await this._readProposalUseCase.FindByProposalNumber(proposalNumber);

                if (returnReadProposal == null)
                {
                    return BadRequest("Failed to Find proposal.");
                }

                return Ok(returnReadProposal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> InsertProposal(RequestInsertProposalDTO requestInsertProposalDTO)
        {
            try
            {
                bool returnInsertProposal = await this._insertProposalUseCase.Insert(requestInsertProposalDTO);

                if (returnInsertProposal == null)
                {
                    return BadRequest("Failed to insert Proposal.");
                }

                return Ok(returnInsertProposal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateProposal(RequestUpdateProposalDTO requestUpdateProposalDTO)
        {
            try
            {
                bool returnUpdateProposal = await this._updateProposalUseCase.Update(requestUpdateProposalDTO);

                if (returnUpdateProposal == null)
                {
                    return BadRequest("Failed to update Proposal.");
                }

                return Ok(returnUpdateProposal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProposal(Guid proposalId)
        {
            try
            {
                bool returnDeleteProposal = await this._deleteProposalUseCase.Delete(proposalId);

                if (returnDeleteProposal == null)
                {
                    return BadRequest("Failed to delete Proposal.");
                }

                return Ok(returnDeleteProposal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
