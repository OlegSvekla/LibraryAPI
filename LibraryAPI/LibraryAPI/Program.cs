using LibraryAPI.Configuration;
using LibraryAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

ConfigureCoreServices.ConfigureServices(builder.Configuration, builder.Services, builder.Logging);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger("LibraryDbContextSeed");

logger.LogInformation("Database migration running...");

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;

    try
    {
        var libraryContext = scopedProvider.GetRequiredService<LibraryDbContext>();

        if (libraryContext.Database.IsSqlServer())
        {
            // Попытка выполнить миграцию базы данных
            var retryCount = 0;
            var maxRetryCount = 10;
            var migrationFailed = false;

            while (retryCount < maxRetryCount)
            {
                try
                {
                    libraryContext.Database.Migrate();
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"Migration failed. Retrying... (Attempt {retryCount + 1}/{maxRetryCount})");
                    logger.LogError(ex.Message);
                    retryCount++;
                    await Task.Delay(1000); // Пауза между попытками
                }
            }

            if (retryCount == maxRetryCount)
            {
                logger.LogError($"Migration failed after {maxRetryCount} attempts. Aborting.");
                migrationFailed = true;
            }

            if (!migrationFailed)
            {
                LibraryDbContextSeed.SeedData(libraryContext);
            }
        }
        else
        {
            logger.LogError("Cannot run database migration. The database provider is not SQL Server.");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while adding migrations to the database.");
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
