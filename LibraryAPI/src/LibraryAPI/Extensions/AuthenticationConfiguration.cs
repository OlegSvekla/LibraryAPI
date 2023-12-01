using IdentityServer4.AccessTokenValidation;

namespace LibraryAPI.Extensions
{
    public static class AuthenticationConfiguration
    {
        public static void Configuration(
            IConfiguration configuration,
            IServiceCollection services)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = configuration["IdentityServer:Authority"];
                        options.ApiName = configuration["IdentityServer:ApiName"];
                        options.RequireHttpsMetadata = false;
                    });
        }
    }
}