using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using CloneBE.Domain.Model;

namespace CloneBE.Domain.InterfaceRepo
{
    public  interface IProductRepo : IGennericRepo<Product>
    {
        Task<IEnumerable<Product>>GetAllProductByCategpry(int categpryId);
        //Task<Product?>GetProductByID(int id);
        Task<Product> GetProductsByIdsAsync(int productIds);

        Task<(List<Product>, int)> GetFilteredProductsAsync(ProductFilterParameters request);


    }
}
