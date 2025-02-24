using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.InterfaceRepo;
using CloneBE.Infraction.Presistences;

namespace CloneBE.Infraction.Repo
{
    public  class UnitOfWork : IUnitOfWork1
    {
        private readonly Databasese databasese;

        public IProductRepo ProductRepo { get; }

       public UnitOfWork(Databasese databasese,IProductRepo productRepo)
        {
            this.databasese = databasese;
            ProductRepo = productRepo;
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
