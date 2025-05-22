using ECommerce.Application.Interfaces.Repositories.RoleRepository;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.RoleRepo
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext db) : base(db) { }

        public async Task<List<string>> GetRoleUserAsync(User user)
        {
            return await _db.UserRoles
                .Where(t => t.UserId == user.Id)
                .Include(t => t.Role)
                .Select(t => t.Role.Name)
                .ToListAsync();
        }
    }
}
