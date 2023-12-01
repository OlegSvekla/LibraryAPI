using FluentValidation;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Interfaces.IRepository;
using LibraryAPI.Domain.Interfaces.IService;
using LibraryAPI.Infrastructure.Data;
using LibraryAPI.Infrastructure.Mapper;
using LibraryAPI.BL.Services;
using LibraryAPI.Domain.Validation;

namespace LibraryAPI.Extensions
{
    public class ServicesConfiguration
    {
        public static void Configuration(
            IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IBookService<BookDto>, BookService>();

            services.AddScoped<IValidator<BookDto>, BookDtoValidator>();
            services.AddScoped<IValidator<AuthorDto>, AuthorDtoValidator>();

            services.AddAutoMapper(typeof(MapperEntityToDto));
        }
    }
}