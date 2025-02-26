using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;
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

        //public async Task<Product?> GetProductByID(int id)
        //{
        //    var tmp = await _dbset.Where(x => x.ProductId == id).FirstOrDefaultAsync();
        //    return tmp;
        //}
    }
}
