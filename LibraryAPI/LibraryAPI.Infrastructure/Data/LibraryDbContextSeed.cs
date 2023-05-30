using LibraryAPI.Core.Entities;
using Microsoft.Extensions.Logging;

namespace LibraryAPI.Infrastructure.Data
{
    public class LibraryDbContextSeed
    {
        private readonly ILogger<LibraryDbContextSeed> _logger;

        public LibraryDbContextSeed(ILogger<LibraryDbContextSeed> logger)
        {
            _logger = logger;
        }
        public static async Task SeedAsync(LibraryDbContext dbContext, ILogger<LibraryDbContextSeed> logger)
        {
            if(!dbContext.Authors.Any())
            {
                var authors = new List<Author>
                {
                    new Author
                    {
                        FirstName = "Andrew",
                        LastName = "Lock",
                       
                    },
                    new Author
                    {
                        FirstName = "Jon",
                        LastName = "Smith",
                        
                    },
                    new Author
                    {
                        FirstName = "Adam",
                        LastName = "Freeman",
                       
                    }
                };

                await dbContext.Authors.AddRangeAsync(authors);
                
            }

            if (!dbContext.Books.Any())
            {
                var books = new List<Book>
                {
                    new Book
                    {
                        AuthorId = 1,
                        Title = "ASP.Net Core In Action",
                        Genre = "Жанр 1",
                        Description = "A particle guide on the ASP.Net Core framework.",
                        Isbn = "978-1617294617",
                        BorrowedDate = DateTime.Now,
                        ReturnDate = DateTime.Now.AddDays(7)



                    },
                    new Book
                    {
                        AuthorId = 2,
                        Title = "Entity Framework Core In Action",
                        Genre = "Жанр 1",
                        Description = "This teaches you how to access and update relational data from .NET applications. Following the crystal-clear explanations, real-world examples, and around 100 diagrams, you’ll discover time-saving patterns and best practices for security, performance tuning, and unit testing.",
                        Isbn = "978-1617294563",
                        BorrowedDate = DateTime.Now,
                        ReturnDate = DateTime.Now.AddDays(14)
                    },
                    new Book
                    {
                        AuthorId = 3,
                        Title = "Pro ASP.Net Core 3",
                        Genre = "Жанр 1",
                        Description = "Now in its 8th edition, the comprehensive book you need to learn ASP.NET Core development!",
                        Isbn = "978-1484254394",
                        BorrowedDate = DateTime.Now,
                        ReturnDate = DateTime.Now.AddDays(14)
                    },
                    new Book
                    {
                        AuthorId = 4,
                        Title = "Pro React 16",
                        Genre = "Жанр 1",
                        Description = "Use this book to build dynamic JavaScript applications using the popular React library.",
                        BorrowedDate = DateTime.Now,
                        ReturnDate = DateTime.Now.AddDays(14)
                    }
                };

                await dbContext.Books.AddRangeAsync(books);
                
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
