using LibraryAPI.Core.Entities;
using LibraryAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryAPI.Infrastructure.Data
{
    public class LibraryDbContextSeed
    {
        public static async Task SeedAsyncData(LibraryDbContext catalogContext, ILogger logger, int retry = 0)
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
                    await SeedAsyncData(catalogContext, logger, retryForAvailbility);
                }
                throw;
            }            
        }

        private static IEnumerable<Author> GetPreConfiguredAuthors()
        {
            return new List<Author>
            {
                new Author("John", "Doe", "American", new DateTime(1990, 1, 15)),
                new Author("Jane", "Smith", "British", new DateTime(1985, 5, 3)),
                new Author("Michael", "Johnson", "American", new DateTime(1978, 9, 20)),
                new Author("David", "Brown", "Canadian", new DateTime(1982, 7, 8)),
                new Author("Emily", "Wilson", "Australian", new DateTime(1995, 11, 12))
            };
        }

        private static IEnumerable<Book> GetPreConfiguredBooks()
        {
            return new List<Book>
            {
                new(1, "Book 1", "1234567890", "Fiction", "Book 1 Description", 300, 19.99m,
                "English", "publishing house 1", new DateTime(2022, 1, 15), DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10)),
                new(2, "Book 2", "0987654321", "Non-Fiction", "Book 2 Description", 250, 15.99m,
                "English", "publishing house 2", new DateTime(2021, 6, 5), DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5)),
                new(3, "Book 3", "9876543210", "Mystery", "Book 3 Description", 400, 24.99m,
                "English", "publishing house 3", new DateTime(2023, 3, 20), DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7)),
                new(4, "Book 4", "5678901234", "Science Fiction", "Book 4 Description", 320, 17.99m,
                "English", "publishing house 4", new DateTime(2020, 11, 10), DateTime.Now.AddDays(-3), DateTime.Now.AddDays(3)),
                new(5, "Book 5", "4321098765", "Thriller", "Book 5 Description", 280, 16.49m,
                "English", "publishing house 5", new DateTime(2019, 8, 25), DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2))
            };
        }
    }
}