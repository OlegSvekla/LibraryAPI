using LibraryAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.ApplicationBuilderExtensions
{
    public static class SeedExtensions
    {
        public static IApplicationBuilder UseLibraryDbContextSeed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var logger = serviceProvider.GetRequiredService<ILogger<LibraryDbContextSeed>>();

                logger.LogInformation("Database migration running...");

                try
                {
                    var context = serviceProvider.GetRequiredService<LibraryDbContext>();
                    context.Database.Migrate();
                    LibraryDbContextSeed.SeedAsyncData(context, logger).Wait();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            return app;
        }
    }
}