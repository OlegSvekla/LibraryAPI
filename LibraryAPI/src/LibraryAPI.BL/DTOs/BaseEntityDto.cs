using Swashbuckle.AspNetCore.Annotations;

namespace LibraryAPI.BL.DTOs
{
    public abstract class BaseEntityDto
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
    }
}
