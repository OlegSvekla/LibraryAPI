using LibraryAPI.Infrastructure.Data;
using LibraryAPI.Core.Interfaces.IRepository;
using LibraryAPI.Core.Interfaces.IService;
using LibraryAPI.Services;
using LibraryAPI.Core.Entities;
using LibraryAPI.Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Configuration
{
    public static class ConfigureCoreServices
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services,
            ILoggingBuilder logging)
        {




            services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("LibraryConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()),
    ServiceLifetime.Scoped);

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));


            services.AddScoped<IBookService<BookDto>, BookService>();


            services.AddAutoMapper(typeof(MapperEntityToDto));

            
        }

    }
}
