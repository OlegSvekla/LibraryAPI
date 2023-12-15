using TaskTracker.Domain.Entities;

namespace LibraryAPI.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public Author()
        {
        }

        public Author(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;;
        }
    }
}