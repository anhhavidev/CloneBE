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
        Task<ProductResponse> GetProductByID(int id);
        Task<ProductResponse> CreateProduct(ProductRequest productRequest);
        Task<ProductResponse> UpdateProduct(ProductUpdateRequest productDetail);
        Task<bool>DeleteProduct(int id);
        Task<IEnumerable<ProductDTO>>GetAllProductByCategory(int categoryId);
        Task<ProductFilterResponse<ProductDTO>> GetFilteredProductsAsync(ProductFilterRequestDTO request);

        //Task<IEnumerable<H> GetAll(string search, double? from, double? to, string sortBy, int page = 1)
    }
}
