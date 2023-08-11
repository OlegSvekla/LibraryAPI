using LibraryAPI.Core.Entities;
using LibraryAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryAPI.Infrastructure.Data
{
    public class LibraryDbContextSeed
    {
        public static async Task SeedData(LibraryDbContext catalogContext, ILogger logger, int retry = 0)
        {
            var retryForAvailbility = retry;

            try
            {
                logger.LogInformation("Data seeding started.");

                if (!await catalogContext.Authors.AnyAsync())
                {
                    await catalogContext.Authors.AddRangeAsync(GetPreConfiguredAuthors());

                    await catalogContext.SaveChangesAsync();
                }
                if (!await catalogContext.Books.AnyAsync())
                {
                    await catalogContext.Books.AddRangeAsync(GetPreConfiguredBooks());

                    await catalogContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailbility >= 10) throw;
                {
                    retryForAvailbility++;

                    logger.LogError(ex.Message);
                    await SeedData(catalogContext, logger, retryForAvailbility);
                }
                throw;
            }            
        }

        private static IEnumerable<Author> GetPreConfiguredAuthors()
        {
            return new List<Author>
                {
                    new("John", "Doe"),
                    new("Jane", "Smith"),
                    new("Michael", "Johnson"),
                    new("David", "Brown"),
                    new("Emily", "Wilson")
                };
        }

        private static IEnumerable<Book> GetPreConfiguredBooks()
        {
            return new List<Book>
                {
                    new(1, "Book 1", "1234567890", "Fiction", "Book 1 Description", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10)),
                    new(2, "Book 2", "0987654321", "Non-Fiction", "Book 2 Description", DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5)),
                    new(3, "Book 3", "9876543210", "Mystery", "Book 3 Description", DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7)),
                    new(4, "Book 4", "5678901234", "Science Fiction", "Book 4 Description", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(3)),
                    new(5, "Book 5", "4321098765", "Thriller", "Book 5 Description", DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2))
                };
        }
    }
}