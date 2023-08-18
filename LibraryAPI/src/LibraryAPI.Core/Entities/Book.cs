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
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime BorrowedDate { get; set; }
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