using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CloneBE.Application.DTO;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;

namespace CloneBE.Application.Interface.Serivce
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork1 unitOfWork1;
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork1 unitOfWork1 ,IMapper mapper) {
            this.unitOfWork1 = unitOfWork1;
            this.mapper = mapper;
        }
        public async Task<CategoryDTO> CreateCategory(CategoryDTO categoryDTO)
        {
            var categorydto = mapper.Map<Category>(categoryDTO);
            unitOfWork1.CategoryRepo.Add(categorydto); //entitifaremwork theo dõi đói tương 
           await unitOfWork1.SaveChangesAsync();
          return mapper.Map<CategoryDTO>(categorydto);
        }

        public async  Task<bool> DeleteCategory(int id)
        {
            var category =  await unitOfWork1.CategoryRepo.GetById(id);
            if(category==null)return false;
            unitOfWork1.CategoryRepo.Delete(category);
            await unitOfWork1.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryDTO>> GetALLCategory()
        {
            var categorys = await unitOfWork1.CategoryRepo.GetAll();
            return mapper.Map<IEnumerable<CategoryDTO>>(categorys);
        }

        public async Task<CategoryDTO?> GetCategoryById(int id)
        {
          var category = await unitOfWork1 .CategoryRepo.GetById(id);
           if(category==null)return null;
           return mapper.Map<CategoryDTO?>(category);
        }

        public async Task<CategoryDTO> UpdateCategory(CategoryDTO categoryDTO)
        {
           var category = mapper.Map<Category>(categoryDTO);
            unitOfWork1.CategoryRepo.Update(category);
           await unitOfWork1.SaveChangesAsync();
            return mapper.Map<CategoryDTO>(category);
        }
    }
}
