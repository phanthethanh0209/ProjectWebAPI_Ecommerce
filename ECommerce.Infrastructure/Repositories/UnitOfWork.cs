using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Repositories.CartRepository;
using ECommerce.Application.Interfaces.Repositories.CouponRepository;
using ECommerce.Application.Interfaces.Repositories.OrderRepository;
using ECommerce.Application.Interfaces.Repositories.PermissionRepository;
using ECommerce.Application.Interfaces.Repositories.ProductRepository;
using ECommerce.Application.Interfaces.Repositories.RoleRepository;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories.CartRepo;
using ECommerce.Infrastructure.Repositories.CouponRepo;
using ECommerce.Infrastructure.Repositories.OrderRepo;
using ECommerce.Infrastructure.Repositories.PermissionRepo;
using ECommerce.Infrastructure.Repositories.ProductRepo;
using ECommerce.Infrastructure.Repositories.RoleRepo;
using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerce.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _db;
        private IDbContextTransaction? _transaction;
        private bool _disposed = false;

        public IGenericRepository<User> User { get; private set; }

        public IGenericRepository<RefreshToken> RefreshToken { get; private set; }

        public IGenericRepository<Category> Category { get; private set; }
        public ICartRepository Carts { get; private set; }

        public IProductRepository Product { get; private set; }

        public IGenericRepository<CartItem> CartItem { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public IGenericRepository<OrderItem> OrderItem { get; private set; }

        public IGenericRepository<Payment> Payment { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public IPermissionRepository Permissions { get; private set; }
        public IGenericRepository<UserRole> UserRole { get; private set; }
        public IGenericRepository<RolePermission> RolePermission { get; private set; }
        public ICouponRepository Coupons { get; private set; }
        public IGenericRepository<Product_Coupons> Product_Coupon { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            User = new GenericRepository<User>(db);
            RefreshToken = new GenericRepository<RefreshToken>(_db);
            Category = new GenericRepository<Category>(_db);
            Carts = new CartRepository(_db);
            Product = new ProductRepository(_db);
            CartItem = new GenericRepository<CartItem>(_db);
            Orders = new OrderRepository(_db);
            OrderItem = new GenericRepository<OrderItem>(_db);
            Payment = new GenericRepository<Payment>(_db);
            Roles = new RoleRepository(_db); ;
            Permissions = new PermissionRepository(_db);
            UserRole = new GenericRepository<UserRole>(_db);
            RolePermission = new GenericRepository<RolePermission>(_db);
            Coupons = new CouponRepository(_db);
            Product_Coupon = new GenericRepository<Product_Coupons>(_db);
        }


        public async Task SaveChangesAsync()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _db.Dispose(); // giai phong DbContext
            }
            _disposed = true;
        }

        public async Task BeginTransactionAsync()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(UnitOfWork));
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            if (_transaction == null)
                throw new InvalidOperationException("No transaction has been started");

            try
            {
                await _db.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await _transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            if (_transaction == null)
                throw new InvalidOperationException("No transaction has been started");

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}
