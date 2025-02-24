using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneBE.Domain.InterfaceRepo
{
    public interface IUnitOfWork1 :IDisposable
    {
        IProductRepo ProductRepo { get; }
        Task<int> SaveChangesAsync();
    }
}
