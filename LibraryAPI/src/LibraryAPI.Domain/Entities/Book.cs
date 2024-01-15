using TaskTracker.Domain.Entities;

namespace LibraryAPI.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }

        public DateTime BorrowedDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public int AuthorId { get; set; } 
        public Author Author { get; set; }

        public Book() { }

        public Book(
            int authorId,
            string title,
            string isbn,
            string genre,
            string description,
            DateTime borrowedDate,
            DateTime returnDate)           
        {
            AuthorId = authorId;
            Title = title;
            Isbn = isbn;
            Genre = genre;
            Description = description;
            BorrowedDate = borrowedDate;
            ReturnDate = returnDate;
        }
    }
}