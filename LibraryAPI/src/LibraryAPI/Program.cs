using LibraryAPI.ConfigurationForServices;
using LibraryAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureCoreServices.ConfigureServices(builder.Configuration, builder.Services);

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
        var catalogContext = scopedProvider.GetRequiredService<LibraryDbContext>();
        if (catalogContext.Database.IsSqlServer())
        {
            catalogContext.Database.Migrate();
        }
        await LibraryDbContextSeed.SeedAsyncData(catalogContext, app.Logger);       
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred adding migrations to Databse.");
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