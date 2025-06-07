using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;
using CloneBE.Domain.Model;
using CloneBE.Infraction.Presistences;
using Microsoft.EntityFrameworkCore;

namespace CloneBE.Infraction.Repo
{
    public class ProductRepo : GennerticRepo<Product>, IProductRepo
    {
        public ProductRepo(Databasese database) : base(database)
        {

        }

      

        public async Task<IEnumerable<Product>> GetAllProductByCategpry(int categpryId)
        {
           return await _dbset.Where(x=>x.CategoryId==categpryId).ToListAsync();
        }

        public async Task<(List<Product>, int)> GetFilteredProductsAsync(ProductFilterParameters request)
        {
            IQueryable<Product> query = _dbset;

            // 🔍 **Tìm kiếm theo tên**
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var lowerSearchTerm = request.SearchTerm.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(lowerSearchTerm));
            }


            //// 📌 **Lọc theo danh mục**
            //if (!string.IsNullOrWhiteSpace(request.Category))
            //{
            //    query = query.Where(p => p.category == request.Category);
            //}

            // 🔢 **Lọc theo khoảng giá**
            if (request.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= request.MinPrice.Value);
            }
            if (request.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= request.MaxPrice.Value);
            }

            // 🔃 **Sắp xếp**
            // 🔃 **Sắp xếp**
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                query = request.SortBy.ToLower() switch
                {
                    "name" => request.IsDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                    "price" => request.IsDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                    //"createddate" => request.IsDescending ? query.OrderByDescending(p => p.CreatedDate) : query.OrderBy(p => p.CreatedDate),
                    _ => query // Nếu SortBy không hợp lệ, không sắp xếp
                };
            }


            // 🔢 **Lấy tổng số sản phẩm**
            int totalCount = await query.CountAsync();

            // 📌 **Phân trang**
            var products = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return (products, totalCount);
        }


        public async  Task<Product> GetProductsByIdsAsync(int productIds)
        {
            return await _dbset.FirstOrDefaultAsync(x => x.ProductId == productIds);
        }

       

        //public async Task<Product?> GetProductByID(int id)
        //{
        //    var tmp = await _dbset.Where(x => x.ProductId == id).FirstOrDefaultAsync();
        //    return tmp;
        //}


    }
}
