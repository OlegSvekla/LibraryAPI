using LibraryAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.ApplicationBuilderExtensions
{
    public static class MigrationsConfiguration
    {
        public static async Task<IApplicationBuilder> RunDbContextMigrations(this IApplicationBuilder app)
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
                    await LibraryDbContextSeed.SeedAsyncData(context, logger);
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