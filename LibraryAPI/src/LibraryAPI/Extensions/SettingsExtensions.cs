using LibraryAPI.Infrastructure.Settings;

namespace LibraryAPI.Extensions
{
    public static class SettingsExtensions
    {
        public static void ConfigureSettings(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("Auth"));
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));
        }
    }
}