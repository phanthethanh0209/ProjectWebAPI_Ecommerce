using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.Infrastructure.Authentication
{
    public class HasPermissionAttribute : TypeFilterAttribute
    {
        public HasPermissionAttribute(string[] permission) : base(typeof(PermissionAuthorizationFilter))
        {
            Arguments = new object[] { new PermissionRequirement(permission) };
        }

        private class PermissionAuthorizationFilter : IAuthorizationFilter
        {
            private readonly IAuthorizationService _authorizationService;
            private readonly PermissionRequirement _permissionRequirement;

            public PermissionAuthorizationFilter(IAuthorizationService authorizationService,
                PermissionRequirement permissionRequirement)
            {
                _authorizationService = authorizationService;
                _permissionRequirement = permissionRequirement;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                // Kiểm tra xác thực
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                // Thực hiện kiểm tra quyền
                AuthorizationResult result = _authorizationService.AuthorizeAsync(context.HttpContext.User, null, _permissionRequirement).Result;
                if (!result.Succeeded)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
