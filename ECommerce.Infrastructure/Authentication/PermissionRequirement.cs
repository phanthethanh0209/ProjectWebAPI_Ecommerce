using Microsoft.AspNetCore.Authorization;

namespace ECommerce.Infrastructure.Authentication
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string[] Permissions { get; set; }

        public PermissionRequirement(string[] permissions)
        {
            Permissions = permissions;
        }
    }
}
