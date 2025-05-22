using ECommerce.Application.Interfaces.Repositories.CartRepository;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.CartRepo
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext db) : base(db) { }


        public async Task<Cart?> GetCartByUserIdWithItemsAsync(Guid userId)
        {
            return await _db.Carts
                .Include(x => x.CartItems)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(t => t.UserId == userId);
        }

        public async Task<CartItem?> GetCartItemByProductIdAsync(Guid cartId, Guid productId)
        {
            return await _db.CartItems.FirstOrDefaultAsync(x => x.CartId == cartId && x.ProductId == productId);
        }

        public async Task ClearCartAsync(Guid cartId)
        {
            List<CartItem> items = await _db.CartItems.Where(t => t.CartId == cartId).ToListAsync();
            _db.CartItems.RemoveRange(items);
        }

        public async Task RemoveCartItemsAsync(Guid cartId, List<Guid> productIds)
        {
            List<CartItem> items = await _db.CartItems
                .Where(t => t.CartId == cartId && productIds.Contains(t.ProductId))
                .ToListAsync();

            _db.CartItems.RemoveRange(items);
            //_db.CartItems.Sum(p => p.Quantity * p.Product.Price);
        }
    }
}
