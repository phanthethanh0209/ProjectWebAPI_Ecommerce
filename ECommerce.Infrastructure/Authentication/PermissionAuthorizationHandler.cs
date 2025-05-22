using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ECommerce.Infrastructure.Authentication
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionAuthorizationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            Claim? userClaim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userClaim == null || !Guid.TryParse(userClaim.Value, out Guid userId))
            {
                return;
            }

            List<Permission> permissions = await _unitOfWork.Permissions.GetPermissionsAsync(userId);
            if (permissions.Any(r => requirement.Permissions.Contains(r.Name)))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
