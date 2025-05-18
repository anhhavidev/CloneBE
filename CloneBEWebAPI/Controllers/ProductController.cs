using CloneBE.Application.DTO;
using CloneBE.Application.DTO.Request;
using CloneBE.Application.Interface.Serivce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloneBEWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
   
        // Lấy tất cả sản phẩm
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await _productService.GellALLProduct();
            return Ok(products);
        }
        [HttpGet("category/{categoryid}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProductByCategory(int categoryid)
        {
            var products = await  _productService.GetAllProductByCategory(categoryid);
            return Ok(products);
        }
        // Lấy sản phẩm theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductRequest>> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByID(id);
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 nếu không tìm thấy
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredProducts([FromQuery] ProductFilterRequestDTO request)
        {
            var result = await _productService.GetFilteredProductsAsync(request);
            return Ok(result);
        }

        // Thêm sản phẩm mới
        [HttpPost("create")]
      
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromForm] ProductRequest productDetail)
        {
            if (productDetail == null) return BadRequest("Dữ liệu không hợp lệ.");

            var createdProduct = await _productService.CreateProduct(productDetail);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, createdProduct);
        }


        // PUT: api/products/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductResponse>> UpdateProduct(int id, [FromForm] ProductUpdateRequest productDetail)
        {
            if (productDetail == null || productDetail.ProductId != id)
                return BadRequest("Dữ liệu không hợp lệ hoặc Id không khớp.");

            var updatedProduct = await _productService.UpdateProduct(productDetail);
            if (updatedProduct == null)
                return NotFound("Sản phẩm không tồn tại.");

            return Ok(updatedProduct);
        }


        // Xóa sản phẩm
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (!result) return NotFound("Sản phẩm không tồn tại.");

            return Ok(true);
        }
    }
}
