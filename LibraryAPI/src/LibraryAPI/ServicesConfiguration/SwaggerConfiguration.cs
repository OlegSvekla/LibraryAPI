using Microsoft.OpenApi.Models;
using System.Reflection;

namespace LibraryAPI.ServicesConfiguration
{
    public static class SwaggerConfiguration
    {
        public static void Configuration(IServiceCollection services)
        {
            services.AddSwaggerGen();
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
        }
    }
}