using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneBE.Domain.InterfaceRepo
{
    public  interface IGennericRepo<T>
    {
        void Add(T enity);
        void Update(T enity);
        void Delete(T enity);
        Task<IEnumerable<T>> GetAll();
        Task<T?>GetById(int id);
    }
}
