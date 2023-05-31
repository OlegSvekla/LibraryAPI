using LibraryAPI.Infrastructure.Data;
using LibraryAPI.Core;
using LibraryAPI.Core.Interfaces.IService;
using LibraryAPI.Infrastructure.Services;
using LibraryAPI.Core.Interfaces.IRepository;
using LibraryAPI.Core.Entities;

namespace MeetupAPI.Configuration
{
    public static class ConfigureCoreServices
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services,
            ILoggingBuilder logging)
        {








            #region Services

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));


            //services.AddAutoMapper(typeof(MapperProfile));

            #endregion
        }
            
    }
}
