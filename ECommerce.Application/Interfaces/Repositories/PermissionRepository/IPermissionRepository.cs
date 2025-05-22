using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories.PermissionRepository
{
    public interface IPermissionRepository
    {
        Task<List<Permission>> GetPermissionsAsync(Guid userId);

    }
}
