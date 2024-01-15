using LibraryAPI.BL.DTOs;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Entities.Auth;
using LibraryAPI.Domain.Requests;
using LibraryAPI.Domain.Responses;

namespace LibraryAPI.Infrastructure.Mapper
{
    public sealed class MapperEntityToDto : AutoMapper.Profile
    {
        public MapperEntityToDto()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<User, AuthRegisterRequest>().ReverseMap();
            CreateMap<User, UserDetailsResponse>().ReverseMap();
        }
    }
}