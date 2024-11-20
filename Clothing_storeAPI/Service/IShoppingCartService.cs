using Clothing_storeAPI.Models;

namespace Clothing_storeAPI.Service
{
    public interface IShoppingCartService
    {
        Task AddToCartAsync(string userId, int productId, int quantity);
    }

    //public class ShoppingCartService : IShoppingCartService
    //{
    //    private readonly Context _context;

    //    public ShoppingCartService(Context context)
    //    {
    //        _context = context;
    //    }

    //    public async Task AddToCartAsync(string userId, int productId, int quantity)
    //    {
    //        var cart = await _context.ShoppingCarts.FirstOrDefaultAsync(c => c.UserId == userId);
    //        if (cart == null)
    //        {
    //            // Nếu chưa có giỏ hàng, tạo mới
    //            cart = new ShoppingCart { UserId = userId };
    //            _context.ShoppingCarts.Add(cart);
    //        }

    //        var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
    //        if (cartItem == null)
    //        {
    //            // Nếu sản phẩm chưa có trong giỏ hàng, thêm mới
    //            cartItem = new CartItem { ProductId = productId, Quantity = quantity };
    //            cart.Items.Add(cartItem);
    //        }
    //        else
    //        {
    //            // Nếu sản phẩm đã có, cập nhật số lượng
    //            cartItem.Quantity += quantity;
    //        }

    //        await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
    //    }
    //}
}
