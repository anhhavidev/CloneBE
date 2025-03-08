using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;

namespace CloneBE.Domain.InterfaceRepo
{
    public  interface ICartRepo
    {
        Task<Cart?> CheckUserCart(string userId);
        Task AddCart(Cart cart);
        Task<CartItem?> CheckCartItem(int cartId, int productId);
        Task addCartItem(CartItem cartitem);
        void updateCartItem(CartItem cartitem);

        Task<bool> deletecartitem(int cartitemid);
        Task RemoveCartItemsByIdsAsync(List<int> cartItemIds);
        Task<IEnumerable<CartItem>> GetAllItem(string userid);
    }
}
