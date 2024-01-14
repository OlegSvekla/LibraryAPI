using LibraryAPI.Domain.Entities.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryAPI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;

        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            //var localizer = context.HttpContext.RequestServices.GetService<IStringLocalizer<SharedResource>>()
            //    ?? throw new ArgumentNullException("Localizer");

            var account = (User?)context.HttpContext.Items["Account"];

            if (account == null)
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            if (_roles.Any() && !_roles.Contains(account.Role))
            {
                context.Result = new JsonResult(new { message = "AccessDenied" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}
