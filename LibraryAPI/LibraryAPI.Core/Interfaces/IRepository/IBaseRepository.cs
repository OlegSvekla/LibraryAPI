using LibraryAPI.Core.DTOs;
using LibraryAPI.Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Core.Interfaces.IRepository
{
    public interface IBaseRepository<T> where T : BaseEntitie 
    {

        //Task<IEnumerable<T>> GetAllAsync();
        //Task<T> GetByIdAsync(int id);
        //Task<T> GetByPropertyAsync(string prop);
        //Task<T>? AddAsync(T entity);
        //Task<T> UpdateAsync(T entity);
        //Task<bool> DeleteAsync(int id);

        public Task<IEnumerable<T>> GetAllByAsync(Func<IQueryable<T>,
          IIncludableQueryable<T, object>>? include = null,
          Expression<Func<T, bool>>? expression = null);

        public Task<T> GetOneByAsync(Func<IQueryable<T>,
           IIncludableQueryable<T, object>>? include = null,
           Expression<Func<T, bool>>? expression = null);

        public Task<T> GetOneManyToManyAsync(Func<IQueryable<T>,
          IIncludableQueryable<T, object>>? include = null,
          Expression<Func<T, bool>>? expression = null);

        public Task CreateAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(T entity);

        public Task<bool> SaveChangesAsync();

    }
}
