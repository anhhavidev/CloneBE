using CloneBE.Application.DTO;
using CloneBE.Application.Interface.Serivce;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloneBEWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService) {
            this.categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllCategory()
        {
            var categorys =await categoryService.GetALLCategory();
            return Ok(categorys);

        }
        // ✅ Lấy danh mục theo ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // ✅ Tạo danh mục mới
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Category data is required.");
            }

            var createdCategory = await categoryService.CreateCategory(categoryDTO);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.CategoryId }, createdCategory);
        }

        // ✅ Cập nhật danh mục
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory([FromBody] CategoryDTO categoryDTO)
        {
          

            var updatedCategory = await categoryService.UpdateCategory(categoryDTO);
            return Ok(updatedCategory);
        }

        // ✅ Xóa danh mục
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var isDeleted = await categoryService.DeleteCategory(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
