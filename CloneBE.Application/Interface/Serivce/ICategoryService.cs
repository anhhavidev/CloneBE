using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Application.DTO;

namespace CloneBE.Application.Interface.Serivce
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDTO>> GetALLCategory();
        public Task<CategoryDTO> CreateCategory(CategoryDTO categoryDTO);
        Task<CategoryDTO> UpdateCategory(CategoryDTO categoryDTO);
        Task<bool> DeleteCategory(int id);
        Task<CategoryDTO?>GetCategoryById(int id);
       
    }
}
