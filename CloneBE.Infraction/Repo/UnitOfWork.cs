using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;
using CloneBE.Infraction.Presistences;

namespace CloneBE.Infraction.Repo
{
    public  class UnitOfWork : IUnitOfWork1
    {
        private readonly Databasese databasese;
     

        public IProductRepo ProductRepo { get; }

        public IGennericRepo<Category> CategoryRepo { get; }

        public UnitOfWork(Databasese databasese,IProductRepo productRepo,IGennericRepo<Category> categoryrepo)
        {
            this.databasese = databasese;
            ProductRepo = productRepo;
            CategoryRepo = categoryrepo;
        }

        public void Dispose()
        {
           databasese.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
           return  await databasese.SaveChangesAsync();
        }
    }
}
