using FluentValidation;
using LibraryAPI.BL.Helpers;
using LibraryAPI.BL.Services;
using LibraryAPI.Domain.DTOs;
using LibraryAPI.Domain.Interfaces.IRepository;
using LibraryAPI.Domain.Interfaces.IService;
using LibraryAPI.Domain.Validation;
using LibraryAPI.Infrastructure.Data.Repositories;
using LibraryAPI.Infrastructure.Mapper;
using TaskTracker.Domain.Interfaces.IRepositories;

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
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IBookService<BookDto>, BookService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IEmailService, EmailService>();


            services.AddScoped<IValidator<BookDto>, BookDtoValidator>();
            services.AddScoped<IValidator<AuthorDto>, AuthorDtoValidator>();

            services.AddAutoMapper(typeof(MapperEntityToDto));
        }
    }
}