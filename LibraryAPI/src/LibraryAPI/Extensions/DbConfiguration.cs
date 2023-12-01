using LibraryAPI.Infrastructure.Data;
using LibraryAPI.Domain.Interfaces.IRepository;
using LibraryAPI.Domain.Interfaces.IService;
using LibraryAPI.BL.Services;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using IdentityServer4.AccessTokenValidation;
using FluentValidation;
using LibraryAPI.Domain.Validation;

namespace LibraryAPI.Extensions
{
    public static class DbConfiguration
    {
        public static void Configuration(
            IConfiguration configuration,
            IServiceCollection services)
        {
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LibraryConnection"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()),
                ServiceLifetime.Scoped);
        }
    }
}