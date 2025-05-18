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
         Task<IEnumerable<CategoryDTO>> GetALLCategory();
        Task<CategoryCreate> CreateCategory(CategoryCreate categoryDTO);
        Task<CategoryDTO> UpdateCategory(CategoryDTO categoryDTO);
        Task<bool> DeleteCategory(int id);
       
       
    }
}
