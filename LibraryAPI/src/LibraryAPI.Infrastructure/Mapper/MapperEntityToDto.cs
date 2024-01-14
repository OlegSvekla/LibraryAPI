using LibraryAPI.Domain.DTOs;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Entities.Auth;
using LibraryAPI.Domain.Request;
using LibraryAPI.Domain.Response;

namespace LibraryAPI.Infrastructure.Mapper
{
    public sealed class MapperEntityToDto : AutoMapper.Profile
    {
        public MapperEntityToDto()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<User, RegisterRequest>().ReverseMap();
            CreateMap<User, UserDetailsResponse>().ReverseMap();
        }
    }
}