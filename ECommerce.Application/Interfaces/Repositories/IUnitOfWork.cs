using ECommerce.Application.Interfaces.Repositories.CartRepository;
using ECommerce.Application.Interfaces.Repositories.OrderRepository;
using ECommerce.Application.Interfaces.Repositories.PermissionRepository;
using ECommerce.Application.Interfaces.Repositories.RoleRepository;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> User { get; }
        IGenericRepository<RefreshToken> RefreshToken { get; }
        IGenericRepository<Category> Category { get; }
        //IGenericRepository<Cart> Cart { get; }
        ICartRepository Carts { get; }
        IGenericRepository<Product> Product { get; }
        //IProductRepository<Product> Product { get; }
        IGenericRepository<CartItem> CartItem { get; }
        IOrderRepository Orders { get; }
        IGenericRepository<OrderItem> OrderItem { get; }
        IGenericRepository<Payment> Payment { get; }
        IRoleRepository Roles { get; }
        IPermissionRepository Permissions { get; }
        IGenericRepository<UserRole> UserRole { get; }
        IGenericRepository<RolePermission> RolePermission { get; }

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task SaveChangesAsync();
    }
}
