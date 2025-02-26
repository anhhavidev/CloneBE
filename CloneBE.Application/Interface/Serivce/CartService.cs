using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.InterfaceRepo;

namespace CloneBE.Application.Interface.Serivce
{
    public class CartService :ICartService 
    {
        private readonly IUnitOfWork1 unitOfWork1;

        public CartService(IUnitOfWork1 unitOfWork1) {
            this.unitOfWork1 = unitOfWork1;
        }
    }
}
