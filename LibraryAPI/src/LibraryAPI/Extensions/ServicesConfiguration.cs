using FluentValidation;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Interfaces.IRepository;
using LibraryAPI.Domain.Interfaces.IService;
using LibraryAPI.Infrastructure.Data;
using LibraryAPI.Infrastructure.Mapper;
using LibraryAPI.BL.Services;
using LibraryAPI.Domain.Validation;
using TaskTracker.Domain.Interfaces.IRepositories;
using TaskTracker.Infrastructure.Data.Repositories;
using LibraryAPI.Domain.DTOs;

namespace LibraryAPI.Extensions
{
    public class ServicesConfiguration
    {
        public static void Configuration(
            IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<IBookService<BookDto>, BookService>();

            services.AddScoped<IValidator<BookDto>, BookDtoValidator>();
            services.AddScoped<IValidator<AuthorDto>, AuthorDtoValidator>();

            services.AddAutoMapper(typeof(MapperEntityToDto));
        }
    }
}