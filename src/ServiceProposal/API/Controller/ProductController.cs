using Microsoft.AspNetCore.Mvc;
using Service.DataTransferObjects.ProductDTO.Request;
using Service.DataTransferObjects.ProductDTO.Response;
using Service.UseCases.ProductUseCase.Interfaces;

namespace API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IInsertProductUseCase _insertProductUseCase;
        private readonly IUpdateProductUseCase _updateProductUseCase;
        private readonly IDeleteProductUseCase _deleteProductUseCase;
        private readonly IReadProductUseCase _readProductUseCase;

        public ProductController(IInsertProductUseCase insertProductUseCase, IUpdateProductUseCase updateProductUseCase, IDeleteProductUseCase deleteProductUseCase, IReadProductUseCase readProductUseCase)
        {
            this._insertProductUseCase = insertProductUseCase;
            this._updateProductUseCase = updateProductUseCase;
            this._deleteProductUseCase = deleteProductUseCase;
            this._readProductUseCase = readProductUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseReadProductDTO>>> FindAllProduct()
        {
            try
            {
                List<ResponseReadProductDTO> returnReadProductList = await this._readProductUseCase.FindAll();

                if (returnReadProductList == null)
                {
                    return BadRequest("Failed to Find product.");
                }

                return Ok(returnReadProductList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "FindProductById")]
        public async Task<ActionResult<ResponseReadProductDTO>> FindProductById(Guid productId)
        {
            try
            {
                ResponseReadProductDTO returnReadProduct = await this._readProductUseCase.FindById(productId);

                if (returnReadProduct == null)
                {
                    return BadRequest("Failed to Find product.");
                }

                return Ok(returnReadProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{ids}", Name = "FindProductByProdcutTypeId")]
        public async Task<ActionResult<IEnumerable<ResponseReadProductDTO>>> FindProductByProdcutTypeId(Guid productTypeId)
        {
            try
            {
                List<ResponseReadProductDTO> returnReadProductList = await this._readProductUseCase.FindByProductTypeId(productTypeId);

                if (returnReadProductList == null)
                {
                    return BadRequest("Failed to Find product.");
                }

                return Ok(returnReadProductList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> InsertProduct(RequestInsertProductDTO requestInsertProductDTO)
        {
            try
            {
                bool returnInsertProduct = await this._insertProductUseCase.Insert(requestInsertProductDTO);

                if (returnInsertProduct == null)
                {
                    return BadRequest("Failed to insert product.");
                }

                return Ok(returnInsertProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateProduct(RequestUpdateProductDTO requestUpdateProductDTO)
        {
            try
            {
                bool returnUpdateProduct = await this._updateProductUseCase.Update(requestUpdateProductDTO);

                if (returnUpdateProduct == null)
                {
                    return BadRequest("Failed to update product.");
                }

                return Ok(returnUpdateProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProduct(Guid productId)
        {
            try
            {
                bool returnDeleteProduct= await this._deleteProductUseCase.Delete(productId);

                if (returnDeleteProduct == null)
                {
                    return BadRequest("Failed to delete product.");
                }

                return Ok(returnDeleteProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
