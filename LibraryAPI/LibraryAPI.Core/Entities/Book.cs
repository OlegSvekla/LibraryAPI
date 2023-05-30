using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Core.Entities
{
    [Table("Books")]
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
       
        public DateTime BorrowedDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public Author Author { get; set; }

    }
}
