using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Core.Entities
{
    public class BookDto 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int AuthorId { get; set; }
        public AuthorDto Author { get; set; }
    }
}