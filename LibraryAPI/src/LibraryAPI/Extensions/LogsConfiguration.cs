using Serilog;

namespace LibraryAPI.Extensions
{
    public static class LogsConfiguration
    {
        public static void Configuration(
            IConfiguration configuration,
            ILoggingBuilder logging)
        {
            logging.ClearProviders();
            logging.AddSerilog(
                new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger());
        }
    }
}