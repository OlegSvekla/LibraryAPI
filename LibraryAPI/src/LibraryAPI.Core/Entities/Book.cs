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
        public string ?Title { get; set; }
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
        public DateTime PublishedDate { get; set; }
        public int Pages { get; set; }
        public decimal Price { get; set; }
        public string Language { get; set; }
        public string PublishingHouse { get; set; }

        [ForeignKey("AuthorId")]
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
            int pages,
            decimal price,
            string language,
            string publishingHouse,
            DateTime publishedDate,
            DateTime borrowedDate,
            DateTime returnDate)           
        {
            AuthorId = authorId;
            Title = title;
            Isbn = isbn;
            Genre = genre;
            Description = description;
            Pages = pages;
            Price = price;
            Language = language;
            PublishingHouse = publishingHouse;
            PublishedDate = publishedDate;
            BorrowedDate = borrowedDate;
            ReturnDate = returnDate;

        }
    }
}