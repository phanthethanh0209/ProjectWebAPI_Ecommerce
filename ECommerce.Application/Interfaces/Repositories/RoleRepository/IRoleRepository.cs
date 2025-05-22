using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories.RoleRepository
{
    public interface IRoleRepository
    {
        Task<List<string>> GetRoleUserAsync(User user);
    }
}
