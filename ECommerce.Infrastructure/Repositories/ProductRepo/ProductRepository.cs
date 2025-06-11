using ECommerce.Application.Interfaces.Repositories.ProductRepository;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.ProductRepo
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<List<Guid>> GetExistingProductAsync(List<Guid> productIds)
        {
            return await _db.Products.Where(p => productIds.Contains(p.Id))
                .Select(p => p.Id)
                .ToListAsync();
        }
    }
}
