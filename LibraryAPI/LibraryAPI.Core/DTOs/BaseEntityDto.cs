using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Core.DTOs
{
    public abstract class BaseEntityDto
    {
        
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
    }
}
