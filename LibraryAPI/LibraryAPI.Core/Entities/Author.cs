using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Core.Entities
{
    public class Author : BaseEntitie
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? FullName { get { return $"{FirstName} {LastName}"; } }

        public List<BookDto>? Books { get; set; } = new();

        public Author()
        {
                
        }

        public Author(string firstName, string lastName, string fullName, IList<BookDto> books )
        {
            FirstName = firstName;
            LastName = lastName;
            FirstName = firstName;
            Books.AddRange(books);
        }
    }
}
