using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Application.DTO;
using CloneBE.Application.DTO.Request;
using CloneBE.Domain.EF;

namespace CloneBE.Application.Interface.Serivce
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GellALLProduct();
        Task<ProductRequest> GetProductByID(int id);
        Task<ProductRequest> CreateProduct(ProductRequest product);
        Task<ProductRequest> UpdateProduct(ProductRequest product);
        Task<bool>DeleteProduct(int id);
        Task<IEnumerable<ProductDTO>>GetAllProductByCategory(int categoryId);
        Task<ProductFilterResponse<ProductDTO>> GetFilteredProductsAsync(ProductFilterRequestDTO request);

        //Task<IEnumerable<H> GetAll(string search, double? from, double? to, string sortBy, int page = 1)
    }
}
