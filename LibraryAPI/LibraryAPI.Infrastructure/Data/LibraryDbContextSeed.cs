using LibraryAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryAPI.Infrastructure.Data
{
    public class LibraryDbContextSeed
    {
        private readonly ILogger _logger;

        public LibraryDbContextSeed(ILogger logger)
        {
            _logger = logger;
        }
        public static void SeedData(LibraryDbContext context, ILogger logger)
        {
            logger.LogInformation("Data seeding started.");

            try
            {
                if (!context.Authors.Any())
                {
                    SeedAuthors(context);
                }

                if (!context.Books.Any())
                {
                    SeedBooks(context);
                }

                logger.LogInformation("Data seeding completed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding data.");
            }
        }

        private static void SeedAuthors(LibraryDbContext context)
        {
            var authors = new List<Author>
        {
            new Author("John", "Doe"),
            
            new Author("Jane", "Smith"),
            
            new Author("Michael", "Johnson"),
           
            new Author("David", "Brown"),
            
            new Author("Emily", "Wilson")
            
        };
            
            context.Authors.AddRange(authors);
            context.SaveChanges(); 
            
        }

        private static void SeedBooks(LibraryDbContext context)
        {
            var books = new List<Book>
        {
            new Book
            {
                
                Title = "Book 1",
                Isbn = "1234567890",
                Genre = "Fiction",
                Description = "Book 1 Description",
                BorrowedDate = DateTime.Now.AddDays(-10),
                ReturnDate = DateTime.Now.AddDays(10),
                AuthorId = 1,
            },
            new Book
            {
                Title = "Book 2",
                Isbn = "0987654321",
                Genre = "Non-Fiction",
                Description = "Book 2 Description",
                BorrowedDate = DateTime.Now.AddDays(-5),
                ReturnDate = DateTime.Now.AddDays(5),
                AuthorId = 2,
            },
            new Book
            {
                
                Title = "Book 3",
                Isbn = "9876543210",
                Genre = "Mystery",
                Description = "Book 3 Description",
                BorrowedDate = DateTime.Now.AddDays(-7),
                ReturnDate = DateTime.Now.AddDays(7),
                AuthorId = 3,
            },
            new Book
            {
               
                Title = "Book 4",
                Isbn = "5678901234",
                Genre = "Science Fiction",
                Description = "Book 4 Description",
                BorrowedDate = DateTime.Now.AddDays(-3),
                ReturnDate = DateTime.Now.AddDays(3),
                AuthorId = 4,
            },
            new Book
            {
               
                Title = "Book 5",
                Isbn = "4321098765",
                Genre = "Thriller",
                Description = "Book 5 Description",
                BorrowedDate = DateTime.Now.AddDays(-2),
                ReturnDate = DateTime.Now.AddDays(2),
                AuthorId = 5,
            }
        };

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }

}