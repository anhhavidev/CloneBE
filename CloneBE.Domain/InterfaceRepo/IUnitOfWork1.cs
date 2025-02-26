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
        IGennericRepo<Category> CategoryRepo { get; }
        IProductRepo ProductRepo { get; }
        Task<int> SaveChangesAsync();
    }
}
