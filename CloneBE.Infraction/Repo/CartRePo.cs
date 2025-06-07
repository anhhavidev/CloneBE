using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;
using CloneBE.Infraction.Presistences;
using Microsoft.EntityFrameworkCore;

namespace CloneBE.Infraction.Repo
{
    public class CartRePo : ICartRepo
    {
        private readonly Databasese db;

        public CartRePo(Databasese databasese ) { 
         
           db = databasese;
        }

        public async Task AddCart(Cart cart)
        {
            await db.carts.AddAsync(cart);
        }

        public async Task addCartItem(CartItem cartitem)
        {
            await db.cartItems.AddAsync(cartitem);
        }

        public async Task<CartItem?> CheckCartItem(int cartId, int productId)
        {
            return await db.cartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.Productid == productId);
        }

        public async  Task<Cart?> CheckUserCart(string userId)
        {
            var check = await db.carts.Include(x=>x.cartItems).Where(x => x.UserId == userId).FirstOrDefaultAsync();
            return check;
        }

        public async   Task<bool> deletecartitem(int cartitemid)
        {
            var product = await db.cartItems.FindAsync(cartitemid);
            if (product == null) return false;
             db.cartItems .Remove(product);
            await db.SaveChangesAsync();  // Phải có dòng này để lưu thay đổi
            return true;
        }

        public async Task<IEnumerable<CartItem>> GetAllItem(string  userid)
        {
            return   await db.cartItems.Where(x=>x.Cart.UserId == userid).ToListAsync();
        }

        public async Task<CartItem> GetCartItemById(int cartItemId)
        {
        
            return await db.cartItems
                .Include(c => c.Cart) // 👈 Thêm cái này để kiểm tra quyền sở hữu
                .FirstOrDefaultAsync(c => c.CartitemId == cartItemId);
        }

        

        public async Task RemoveCartItemsByIdsAsync(List<int> cartItemIds)
        {
            var itemsToRemove =  await db.cartItems.Where(ci => cartItemIds.Contains(ci.CartitemId)).ToListAsync();
            db.cartItems.RemoveRange(itemsToRemove);
           
        }


        public void updateCartItem(CartItem cartitem)
        {
              db.cartItems.Update(cartitem);
        }
    }
}
