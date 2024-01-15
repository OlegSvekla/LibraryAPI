using LibraryAPI.Domain.Entities.Auth;
using LibraryAPI.Domain.Interfaces.IServices;
using LibraryAPI.Domain.Requests;
using LibraryAPI.Domain.Responses;
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
        public async Task<ActionResult<UserDetailsResponse>> Authenticate(AuthRegisterRequest model)
        {
            var response = await accountService.Authenticate(model, GetIpAddressFromConnection());

            SetTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthRegisterRequest model)
        {
            if (!IsAnonymousSession)
                throw new ValidationException("Please logout before registration");

            await accountService.RegisterAsync(model, Role.User);

            return Ok(new { message = "Registration successful. Please verify your email" });
        }

        [AllowAnonymous]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailRequest model)
        {
            await accountService.VerifyEmailAsync(model.Token);

            return Ok(new { message = "Verification successful" });
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
                    ?? throw new ApplicationException("Invalid Remote Ip");
        }
    }
}