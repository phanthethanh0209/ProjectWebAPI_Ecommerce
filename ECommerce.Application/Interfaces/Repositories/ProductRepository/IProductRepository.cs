using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories.ProductRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Guid>> GetExistingProductAsync(List<Guid> productIds);
    }
}
