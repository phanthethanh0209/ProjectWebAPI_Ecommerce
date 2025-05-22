using ECommerce.Application.Interfaces.Repositories.PermissionRepository;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.PermissionRepo
{
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(ApplicationDbContext db) : base(db) { }

        public async Task<List<Permission>> GetPermissionsAsync(Guid userId)
        {
            List<Guid> roles = await _db.UserRoles
                                .Where(x => x.UserId == userId)
                                .Select(t => t.RoleId)
                                .ToListAsync();

            return await _db.RolePermissions
                .Where(p => roles.Contains(p.RoleId))
                .Select(t => t.Permission)
                .ToListAsync();
        }
    }
}
