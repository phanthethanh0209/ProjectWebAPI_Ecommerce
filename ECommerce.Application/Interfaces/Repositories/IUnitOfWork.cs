using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> User { get; }
        IGenericRepository<RefreshToken> RefreshToken { get; }
        IGenericRepository<Category> Category { get; }
        IGenericRepository<Cart> Cart { get; }
        IGenericRepository<Product> Product { get; }
        //IProductRepository<Product> Product { get; }
        IGenericRepository<CartItem> CartItem { get; }
        IGenericRepository<Order> Order { get; }
        IGenericRepository<OrderItem> OrderItem { get; }
        IGenericRepository<Payment> Payment { get; }

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task SaveChangesAsync();
    }
}
