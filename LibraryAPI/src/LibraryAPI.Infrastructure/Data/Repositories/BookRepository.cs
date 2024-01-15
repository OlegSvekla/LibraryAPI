﻿using LibraryAPI.Domain.Entities;
using TaskTracker.Domain.Interfaces.IRepositories;

namespace LibraryAPI.Infrastructure.Data.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}