using Swashbuckle.AspNetCore.Annotations;

namespace LibraryAPI.Domain.DTOs
{
    public abstract class BaseEntityDto
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
    }
}
