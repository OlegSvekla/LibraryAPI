namespace LibraryAPI.Domain.DTOs
{
    public class BookDto : BaseEntityDto
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }

        public DateTime BorrowedDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public AuthorDto Author { get; set; }
    }
}