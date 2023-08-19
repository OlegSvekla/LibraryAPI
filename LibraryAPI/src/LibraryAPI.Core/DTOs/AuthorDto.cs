using Swashbuckle.AspNetCore.Annotations;

namespace LibraryAPI.Domain.Entities
{
    public class AuthorDto 
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
}