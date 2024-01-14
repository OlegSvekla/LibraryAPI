using LibraryAPI.BL.Helpers;
using LibraryAPI.Domain.Interfaces.IRepository;

namespace LibraryAPI.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public JwtMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var accountId = jwtUtils.ValidateJwtToken(token);

                if (accountId != null)
                {
                    context.Items["Account"] = await userRepository.GetOneByAsync(expression: _ => _.Id.Equals(accountId.Value));
                }
            }

            await _next(context);
        }
    }
}