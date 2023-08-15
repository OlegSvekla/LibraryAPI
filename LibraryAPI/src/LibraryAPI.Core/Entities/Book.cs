using LibraryAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Core.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Isbn { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime BorrowedDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }

        public int AuthorId { get; set; } 
        public Author Author { get; set; }

        public Book()
        {
                
        }

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