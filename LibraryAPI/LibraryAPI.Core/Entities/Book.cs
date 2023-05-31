using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Core.Entities
{
    public class Book : BaseEntitie
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Isbn { get; set; }

        [Required]
        public string? Genre { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public int AuthorId { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? BorrowedDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? ReturnDate { get; set; }


        public AuthorDto Author { get; set; }
    }
}
