using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Infrastructure.Mapper
{
    public sealed class MapperEntityToDto : AutoMapper.Profile
    {
        public MapperEntityToDto()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Author, AuthorDto>().ReverseMap();
        }
    }
}