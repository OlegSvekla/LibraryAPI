using LibraryAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<LibraryDbContextSeed>>();

logger.LogInformation("Database migration running...");

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;

    try
    {
        var meetupContext = scopedProvider.GetRequiredService<LibraryDbContext>();

        if (meetupContext.Database.IsSqlServer())
        {
            meetupContext.Database.Migrate();
        }

        await LibraryDbContextSeed.SeedAsync(meetupContext, logger);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred adding migrations to Database.");
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
