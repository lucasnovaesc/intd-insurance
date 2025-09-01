using Microsoft.AspNetCore.Mvc;
using Service.DataTransferObjects.CustomerDTO.Request;
using Service.DataTransferObjects.CustomerDTO.Response;
using Service.UseCases.CustomerUseCase.Interfaces;

namespace API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IInsertCustomerUseCase _insertCustomerUseCase;
        private readonly IUpdateCustomerUseCase _updateCustomerUseCase;
        private readonly IDeleteCustomerUseCase _deleteCustomerUseCase;
        private readonly IReadCustomerUseCase _readCustomerUseCase;

        public CustomerController(IInsertCustomerUseCase insertCustomerUseCase, IUpdateCustomerUseCase updateCustomerUseCase, IDeleteCustomerUseCase deleteCustomerUseCase, IReadCustomerUseCase readCustomerUseCase)
        {
            this._insertCustomerUseCase = insertCustomerUseCase;
            this._updateCustomerUseCase = updateCustomerUseCase;
            this._deleteCustomerUseCase = deleteCustomerUseCase;
            this._readCustomerUseCase = readCustomerUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseReadCustomerDTO>>> FindAllCustomer()
        {
            try
            {
                List<ResponseReadCustomerDTO> returnReadCustomerList = await this._readCustomerUseCase.FindAll();

                if (returnReadCustomerList == null)
                {
                    return BadRequest("Failed to Find customers.");
                }

                return Ok(returnReadCustomerList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "FindCustomerById")]
        public async Task<ActionResult<ResponseReadCustomerDTO>> FindCustomerById(Guid customerId)
        {
            try
            {
                ResponseReadCustomerDTO returnReadCustomer = await this._readCustomerUseCase.FindById(customerId);

                if (returnReadCustomer == null)
                {
                    return BadRequest("Failed to Find customer.");
                }

                return Ok(returnReadCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> InsertCustomer(RequestInsertCustomerDTO requestInsertCustomerDTO)
        {
            try
            {
                bool returnInsertCustomer = await this._insertCustomerUseCase.Insert(requestInsertCustomerDTO);

                if (returnInsertCustomer == null)
                {
                    return BadRequest("Failed to insert customer.");
                }

                return Ok(returnInsertCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateCustomer(RequestUpdateCustomerDTO requestUpdateCustomerDTO)
        {
            try
            {
                bool returnUpdateCustomer = await this._updateCustomerUseCase.Update(requestUpdateCustomerDTO);

                if (returnUpdateCustomer == null)
                {
                    return BadRequest("Failed to update customer.");
                }

                return Ok(returnUpdateCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCustomer(Guid customerId)
        {
            try
            {
                bool returnDeleteCustomer = await this._deleteCustomerUseCase.Delete(customerId);

                if (returnDeleteCustomer == null)
                {
                    return BadRequest("Failed to delete customer.");
                }

                return Ok(returnDeleteCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
