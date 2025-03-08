using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Application.DTO.Request;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;

namespace CloneBE.Application.Interface.Serivce
{
    public class CartService :ICartService 
    {
        private readonly IUnitOfWork1 unitOfWork1;

        public CartService(IUnitOfWork1 unitOfWork1) {
            this.unitOfWork1 = unitOfWork1;
        }

        public async Task<bool> AddProductToCart(ProductCartRequest cartRequest, ClaimsPrincipal users)
        {
            // Lấy userId
            var userId = users.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return false;

            // Kiểm tra giỏ hàng của user
            var userCart = await unitOfWork1.cartRepo.CheckUserCart(userId);
            if (userCart == null)
            {
                userCart = new Cart
                {
                    UserId = userId,
                    cartItems = new List<CartItem>()
                };
                await unitOfWork1.cartRepo.AddCart(userCart);
                await unitOfWork1.SaveChangesAsync();
            }

            // Lấy danh sách sản phẩm trong giỏ hàng + sản phẩm mới cần thêm
            // Lấy sản phẩm cần thêm
            var productToAdd = await unitOfWork1.ProductRepo.GetProductsByIdsAsync(cartRequest.productId);
            if (productToAdd == null || productToAdd.stock < cartRequest.quantity)
            {
                return false; // Hết hàng, không thể thêm vào giỏ
            }

         

            // Kiểm tra sản phẩm đã có trong giỏ hàng chưa
            var cartItem = await unitOfWork1.cartRepo.CheckCartItem(userCart.CartId, cartRequest.productId);
            if (cartItem == null)
            {
                // Thêm sản phẩm mới vào giỏ hàng
                cartItem = new CartItem
                {
                    Productid = cartRequest.productId,
                    CartId = userCart.CartId,
                    quanlity = cartRequest.quantity,
                    price = productToAdd.Price // Lấy giá từ sản phẩm
                };
                await unitOfWork1.cartRepo.addCartItem(cartItem);
            }
            else
            {
                // Nếu sản phẩm đã có trong giỏ, cập nhật số lượng
                cartItem.quanlity += cartRequest.quantity;
                unitOfWork1.cartRepo.updateCartItem(cartItem);
            }

            await unitOfWork1.SaveChangesAsync();
            return true;
        }
        public  async Task<IEnumerable<CartItem>> GetallCartItem(ClaimsPrincipal users)
        {
            var userid = users.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userid)) return null;
            var cartitems = await unitOfWork1.cartRepo.GetAllItem(userid);
            return cartitems;

        }

        public async Task<bool> DeleteCartItem(int CartItemId)
        {
            var result = await unitOfWork1.cartRepo.deletecartitem(CartItemId);
            await unitOfWork1.SaveChangesAsync();
            return result; // Trả về kết quả
        }
    }
}
