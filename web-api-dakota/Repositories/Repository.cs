using Microsoft.EntityFrameworkCore;
using web_api_dakota.Data;
using web_api_dakota.Repositories.Interfaces;

namespace web_api_dakota.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly WebDakotaContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(WebDakotaContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity; // Retorna a entidade com ID gerado, se aplicável
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0; // Retorna true se houve alguma mudança
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return false;

        _dbSet.Remove(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0; // Retorna true se a entidade foi removida
    }

    public async Task<bool> DeleteAllAsync()
    {
        var allEntities = await _context.Set<T>().ToListAsync();
        
        if(allEntities == null || allEntities.Count == 0) return false;
        
        _dbSet.RemoveRange(_dbSet);
        await _context.SaveChangesAsync();
        
        return true;
    }
    
}