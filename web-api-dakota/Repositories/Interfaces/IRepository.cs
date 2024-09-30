
using System.Linq.Expressions;

namespace web_api_dakota.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    
    Task<T?> GetByIdAsync(int id);

    Task<T?> GetByIdIncludingAsync(int id, params Expression<Func<T, object>>[] includeProperties);
    
    Task<T> AddAsync(T entity);
    
    Task<bool> UpdateAsync(T entity);
    
    Task<bool> DeleteAsync(int id);
    
    Task<bool> DeleteAllAsync();
}
