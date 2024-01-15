using LibraryApi.BL.Validators.IValidators;
using LibraryAPI.BL.DTOs;
using LibraryAPI.BL.Helpers;
using LibraryAPI.BL.Services;
using LibraryAPI.BL.Validators;
using LibraryAPI.Domain.Interfaces.IRepositories;
using LibraryAPI.Domain.Interfaces.IServices;
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
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IJwtUtils, JwtUtils>();

            services.AddScoped<IAuthRequestValidator, AuthRequestValidator>();
            services.AddScoped<IAuthorDtoValidator, AuthorDtoValidator>();
            services.AddScoped<IBookDtoValidator, BookDtoValidator>();
            services.AddScoped<INameValidator, NameValidator>();

            services.AddAutoMapper(typeof(MapperEntityToDto));
        }
    }
}