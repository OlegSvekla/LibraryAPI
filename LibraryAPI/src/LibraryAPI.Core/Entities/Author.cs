using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Core.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public string Nationality { get; set; }
        public DateTime BirthDate { get; set; }

        public Author()
        {
        }

        public Author(string firstName, string lastName, string nationality, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Nationality = nationality;
            BirthDate = birthDate;
        }
    }
}