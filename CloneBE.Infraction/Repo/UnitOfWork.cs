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

        public ICartRepo cartRepo { get; }
     
        public IOrderRepository OrderRepo { get; }
        public IUserRepository UserRepository { get; }
      

        public UnitOfWork(Databasese databasese,IProductRepo productRepo,IGennericRepo<Category> categoryrepo,ICartRepo cartRepos,IOrderRepository orderRepository,IUserRepository userRepository)

        {
            this.databasese = databasese;
            ProductRepo = productRepo;
            CategoryRepo = categoryrepo;
            cartRepo = cartRepos;
            OrderRepo = orderRepository;
            UserRepository = userRepository;
        }

        public void Dispose()
        {
           databasese.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
           return  await databasese.SaveChangesAsync();
        }

        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public Task RollbackAsync()
        {
            throw new NotImplementedException();
        }
    }
}
