using LibraryAPI.Core.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Core.Entities
{
    [Table("Authors")]
    public class AuthorDto : BaseEntityDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public IList<BookDto> Books { get; set; }

        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }

    }

   
}
