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
        [Authorize]
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
            var product = await _productService.GetProductByID(id);
            if (product == null) return NotFound("Sản phẩm không tồn tại.");
            return Ok(product);
        }

        // Thêm sản phẩm mới
        [HttpPost("create")]
        public async Task<ActionResult<ProductRequest>> CreateProduct([FromBody] ProductRequest productDetail)
        {
            if (productDetail == null) return BadRequest("Dữ liệu không hợp lệ.");

            var createdProduct = await _productService.CreateProduct(productDetail);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, createdProduct);
        }

        // Cập nhật sản phẩm
        [HttpPut("update")]
        public async Task<ActionResult<ProductRequest>> UpdateProduct([FromBody] ProductRequest productDetail)
        {
            if (productDetail == null) return BadRequest("Dữ liệu không hợp lệ.");

            var updatedProduct = await _productService.UpdateProduct(productDetail);
            if (updatedProduct == null) return NotFound("Sản phẩm không tồn tại.");

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
