using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;

namespace CloneBE.Domain.InterfaceRepo
{
    public  interface IProductRepo : IGennericRepo<Product>
    {
        Task<IEnumerable<Product>>GetAllProductByCategpry(int categpryId);
        //Task<Product?>GetProductByID(int id);
       
    }
}
