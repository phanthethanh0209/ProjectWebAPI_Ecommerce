using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories.CartRepository
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart?> GetCartByUserIdWithItemsAsync(Guid userId);
        Task<CartItem?> GetCartItemByProductIdAsync(Guid cartId, Guid productId);
        Task ClearCartAsync(Guid cartId);
    }
}
