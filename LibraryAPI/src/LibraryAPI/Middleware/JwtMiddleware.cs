using LibraryAPI.BL.Helpers;
using LibraryAPI.Domain.Interfaces.IRepositories;

namespace LibraryAPI.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IServiceProvider serviceProvider;

        public JwtMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            this.next = next;
            this.serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var accountId = jwtUtils.ValidateJwtToken(token);

                if (accountId != null)
                {
                    context.Items["Account"] = await userRepository.GetOneByAsync(expression: _ => _.Id.Equals(accountId.Value),
                        cancellationToken: CancellationToken.None);
                }
            }

            await next(context);
        }
    }
}