using Microsoft.AspNetCore.Mvc;
using Service.DataTransferObjects.ProductDTO.Request;
using Service.DataTransferObjects.ProductTypeDTO.Request;
using Service.DataTransferObjects.ProductTypeDTO.Response;
using Service.UseCases.ProductTypeUseCase.Interfaces;

namespace API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IInsertProductTypeUseCase _insertProductTypeUseCase;
        private readonly IUpdateProductTypeUseCase _updateProductTypeUseCase;
        private readonly IDeleteProductTypeUseCase _deleteProductTypeUseCase;
        private readonly IReadProductTypeUseCase _readProductTypeUseCase;

        public ProductTypeController(IInsertProductTypeUseCase insertProductTypeUseCase, IUpdateProductTypeUseCase updateProductTypeUseCase, IDeleteProductTypeUseCase deleteProductTypeUseCase, IReadProductTypeUseCase readProductTypeUseCase)
        {
            this._insertProductTypeUseCase = insertProductTypeUseCase;
            this._updateProductTypeUseCase = updateProductTypeUseCase;
            this._deleteProductTypeUseCase = deleteProductTypeUseCase;
            this._readProductTypeUseCase = readProductTypeUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseReadProductTypeDTO>>> FindAllProductTypes()
        {
            try
            {
                List<ResponseReadProductTypeDTO> returnReadProductTypeList = await this._readProductTypeUseCase.FindAll();

                if (returnReadProductTypeList == null)
                {
                    return BadRequest("Failed to Find product type.");
                }

                return Ok(returnReadProductTypeList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "FindProductTypeById")]
        public async Task<ActionResult<ResponseReadProductTypeDTO>> FindProductTypeById(Guid productTypeId)
        {
            try
            {
                ResponseReadProductTypeDTO returnReadProductType = await this._readProductTypeUseCase.FindById(productTypeId);

                if (returnReadProductType == null)
                {
                    return BadRequest("Failed to Find product type.");
                }

                return Ok(returnReadProductType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> InsertProductType(RequestInsertProductTypeDTO requestInsertProductTypeDTO)
        {
            try
            {
                bool returnInsertProductType = await this._insertProductTypeUseCase.Insert(requestInsertProductTypeDTO);

                if (returnInsertProductType == null)
                {
                    return BadRequest("Failed to insert product type.");
                }

                return Ok(returnInsertProductType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateProductType(RequestUpdateProductTypeDTO requestUpdateProductTypeDTO)
        {
            try
            {
                bool returnUpdateProductType = await this._updateProductTypeUseCase.Update(requestUpdateProductTypeDTO);

                if (returnUpdateProductType == null)
                {
                    return BadRequest("Failed to update product type.");
                }

                return Ok(returnUpdateProductType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProduct(Guid productTypeId)
        {
            try
            {
                bool returnDeleteProductType = await this._deleteProductTypeUseCase.Delete(productTypeId);

                if (returnDeleteProductType == null)
                {
                    return BadRequest("Failed to delete product type.");
                }

                return Ok(returnDeleteProductType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
