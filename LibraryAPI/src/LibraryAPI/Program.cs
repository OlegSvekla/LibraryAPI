using LibraryAPI.ApplicationBuilderExtensions;
using LibraryAPI.ServicesConfiguration;

var builder = WebApplication.CreateBuilder(args);

DbConfiguration.Configuration(builder.Configuration, builder.Services);
ServicesConfiguration.Configuration(builder.Services);
SwaggerConfiguration.Configuration(builder.Services);
AuthenticationConfiguration.Configuration(builder.Configuration, builder.Services);

var app = builder.Build();

app.UseLibraryDbContextSeed();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();