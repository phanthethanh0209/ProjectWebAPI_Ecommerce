using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
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

        public IGenericRepository<Cart> Cart { get; private set; }

        public IGenericRepository<Product> Product { get; private set; }

        public IGenericRepository<CartItem> CartItem { get; private set; }

        public IGenericRepository<Order> Order { get; private set; }

        public IGenericRepository<OrderItem> OrderItem { get; private set; }

        public IGenericRepository<Payment> Payment { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            User = new GenericRepository<User>(db);
            RefreshToken = new GenericRepository<RefreshToken>(_db);
            Category = new GenericRepository<Category>(_db);
            Cart = new GenericRepository<Cart>(_db);
            Product = new GenericRepository<Product>(_db);
            CartItem = new GenericRepository<CartItem>(_db);
            Order = new GenericRepository<Order>(_db);
            OrderItem = new GenericRepository<OrderItem>(_db);
            Payment = new GenericRepository<Payment>(_db);
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
