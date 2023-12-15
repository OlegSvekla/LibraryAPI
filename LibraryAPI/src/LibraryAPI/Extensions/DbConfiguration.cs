using LibraryAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Extensions
{
    public static class DbConfiguration
    {
        public static void Configuration(
            IConfiguration configuration,
            IServiceCollection services)
        {
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LibraryConnection"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()),
                ServiceLifetime.Scoped);
        }
    }
}