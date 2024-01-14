using LibraryAPI.Domain.Entities.Auth;
using LibraryAPI.Domain.Interfaces.IService;
using LibraryAPI.Domain.Request;
using LibraryAPI.Domain.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using AuthorizeAttribute = LibraryAPI.Filters.AuthorizeAttribute;

namespace GelionTransApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAccountService accountService;

        public AuthController(
            IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<UserDetailsResponse>> Authenticate(BaseAuhtRequest model)
        {
            var response = await accountService.Authenticate(model, GetIpAddressFromConnection());

            SetTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            if (!IsAnonymousSession)
                throw new ValidationException("PleaseLogoutBeforeRegistration");

            await accountService.RegisterAsync(model, Role.User);



            return Ok(new { message = "RegistrationSuccessful" });
        }

        [AllowAnonymous]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailRequest model)
        {
            await accountService.VerifyEmailAsync(model.Token);

            return Ok(new { message = "VerificationSuccessful" });
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                SameSite = SameSiteMode.None,
                Secure = true,
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string? GetIpAddressFromConnection()
        {
            return Request.Headers.ContainsKey("X-Forwarded-For")
                ? Request.Headers["X-Forwarded-For"]
                : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()
                    ?? throw new ApplicationException(/*localizer["InvalidRemoteIp"]*/);
        }
    }
}