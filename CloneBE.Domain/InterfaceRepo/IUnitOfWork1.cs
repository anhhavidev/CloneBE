using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;

namespace CloneBE.Domain.InterfaceRepo
{
    public interface IUnitOfWork1 :IDisposable
    {
        ICartRepo cartRepo { get; }
        IGennericRepo<Category> CategoryRepo { get; }
        IProductRepo ProductRepo { get; }
        IOrderRepository OrderRepo { get; }
        Task<int> SaveChangesAsync();
    }
}
