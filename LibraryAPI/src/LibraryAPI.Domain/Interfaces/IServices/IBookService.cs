namespace LibraryAPI.Domain.Interfaces.IServices
{
    public interface IBookService<T> where T : class
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IList<T>> GetByIsbnAsync(string isbn);

        Task<bool> CreateAsync(T book);
        Task<bool> UpdateAsync(int id,T book);
        Task<bool> DeleteAsync(int id);      
    }
}