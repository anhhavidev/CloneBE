using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Application.DTO;
using CloneBE.Domain.EF;

namespace CloneBE.Application.Interface.Serivce
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GellALLProduct();
        Task<ProductDetail> GetProductByID(int id);
        Task<ProductDetail> CreateProduct(ProductDetail product);
        Task<ProductDetail> UpdateProduct(ProductDetail product);
        Task<bool>DeleteProduct(int id);
    }
}
