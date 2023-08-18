using LibraryAPI.Infrastructure.Data;
using LibraryAPI.Core.Interfaces.IRepository;
using LibraryAPI.Core.Interfaces.IService;
using LibraryAPI.Services;
using LibraryAPI.Core.Entities;
using LibraryAPI.Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using IdentityServer4.AccessTokenValidation;
using FluentValidation;
using LibraryAPI.Core.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LibraryAPI.ConfigurationForServices
{
    public static class ConfigureCoreServices
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {

            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LibraryConnection"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()),
                ServiceLifetime.Scoped);

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddScoped<IBookService<BookDto>, BookService>();
            services.AddScoped<IValidator<BookDto>, BookDtoValidator>();
            services.AddScoped<IValidator<AuthorDto>, AuthorDtoValidator>();

            services.AddAutoMapper(typeof(MapperEntityToDto));

            services.AddSwaggerGen(_ =>
            {
                _.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "LibraryAPI",
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                _.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                _.EnableAnnotations();
            });

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme." +
                    "\r\n\r\n Enter 'Bearer' [space] and then your token in the text input.",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    //the URL on which the IdentityServer is up and running
                    options.Authority = configuration["IdentityServer:Authority"];
                    //the name of the WebAPI resource
                    options.ApiName = configuration["IdentityServer:ApiName"];
                    //select false for the development
                    options.RequireHttpsMetadata = false;
                });
        }
    }
}