using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Application.DTO.Request;
using CloneBE.Domain.EF;

namespace CloneBE.Application.Interface.Serivce
{
    public interface ICartService
    {
        Task<bool> AddProductToCart(ProductCartRequest cartRequest, ClaimsPrincipal users);
        Task<bool> DeleteCartItem(int CartItemId);
        Task<IEnumerable<CartItem>> GetallCartItem(ClaimsPrincipal users);
    }
}
