using AutoMapper;
using CloneBE.Application.DTO.Request;
using CloneBE.Application.DTO;
using CloneBE.Application.Interface.Serivce;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;
using System.Security.Claims;

public class CartService : ICartService
{
    private readonly IUnitOfWork1 unitOfWork1;
    private readonly IMapper mapper;

    public CartService(IUnitOfWork1 unitOfWork1, IMapper mapper)
    {
        this.unitOfWork1 = unitOfWork1;
        this.mapper = mapper;
    }

    public async Task<bool> AddProductToCart(ProductCartRequest cartRequest, ClaimsPrincipal users)
    {
        var userId = users.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return false;

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

        var product = await unitOfWork1.ProductRepo.GetProductsByIdsAsync(cartRequest.productId);
        if (product == null || product.stock < cartRequest.quantity) return false;

        var existingItem = await unitOfWork1.cartRepo.CheckCartItem(userCart.CartId, cartRequest.productId);
        if (existingItem == null)
        {
            var newItem = new CartItem
            {
                Productid = cartRequest.productId,
                CartId = userCart.CartId,
                quanlity = cartRequest.quantity,
                price = (double)product.Price
            };
            await unitOfWork1.cartRepo.addCartItem(newItem);
        }
        else
        {
            existingItem.quanlity += cartRequest.quantity;
            unitOfWork1.cartRepo.updateCartItem(existingItem);
        }

        await unitOfWork1.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<CartItemDTO>> GetallCartItem(ClaimsPrincipal users)
    {
        var userId = users.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return null;

        var items = await unitOfWork1.cartRepo.GetAllItem(userId);
        return mapper.Map<IEnumerable<CartItemDTO>>(items);
    }

    public async Task<bool> DeleteCartItem(int cartItemId, ClaimsPrincipal users)
    {
        var userId = users.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return false;

        var item = await unitOfWork1.cartRepo.GetCartItemById(cartItemId);
        if (item == null || item.Cart.UserId != userId) return false;

        var result = await unitOfWork1.cartRepo.deletecartitem(cartItemId);
        await unitOfWork1.SaveChangesAsync();
        return result;
    }

    public async Task<bool> UpdateCartItemQuantity(int cartItemId, int newQuantity, ClaimsPrincipal users)
    {
        var userId = users.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId) || newQuantity <= 0) return false;

        var item = await unitOfWork1.cartRepo.GetCartItemById(cartItemId);
        if (item == null || item.Cart.UserId != userId) return false;

        var product = await unitOfWork1.ProductRepo.GetProductsByIdsAsync(item.Productid);
        if (product == null || product.stock < newQuantity) return false;

        item.quanlity = newQuantity;
        unitOfWork1.cartRepo.updateCartItem(item);
        await unitOfWork1.SaveChangesAsync();
        return true;
    }

    public async Task<double> GetCartTotal(ClaimsPrincipal users)
    {
        var userId = users.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return 0;

        var items = await unitOfWork1.cartRepo.GetAllItem(userId);
        return items.Sum(x => x.price * x.quanlity);
    }
}
