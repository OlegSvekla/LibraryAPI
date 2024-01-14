using LibraryAPI.ApplicationBuilderExtensions;
using LibraryAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSettings();

LogsConfiguration.Configuration(builder.Configuration, builder.Logging);
DbConfiguration.Configuration(builder.Configuration, builder.Services);
ServicesConfiguration.Configuration(builder.Services);
SwaggerConfiguration.Configuration(builder.Services);
AuthenticationConfiguration.Configuration(builder.Configuration, builder.Services);

var app = builder.Build();

await app.RunDbContextMigrations();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddlewares();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();