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
       

        // ✅ Tạo danh mục mới
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] CategoryCreate categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Category data is required.");
            }

            var createdCategory = await categoryService.CreateCategory(categoryDTO);
            return Ok(createdCategory);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
                return BadRequest("Id trong URL không khớp với Id trong dữ liệu gửi lên");

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
            return Ok(new { message = "Xóa thành công" });

        }
    }
}
