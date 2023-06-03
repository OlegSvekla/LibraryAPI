namespace LibraryAPI.Core.Entities
{

    public class AuthorDto 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public List<BookDto> Books { get; set; } = new List<BookDto>();
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
    }


}
