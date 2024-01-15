using LibraryAPI.BL;
using LibraryAPI.Middleware;

namespace LibraryAPI.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
        }
    }
}
