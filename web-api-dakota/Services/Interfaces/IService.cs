using System.Linq.Expressions;

namespace web_api_dakota.Services.Interfaces;

public interface IService<T, TRequestDto, TResponseDto, TUpdateDto> where T : class
{
    Task<IEnumerable<TResponseDto>> GetAllAsync();
    
    Task<TResponseDto?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    
    Task<TResponseDto> AddAsync(TRequestDto requestDto, object? relatedEntity = null); 
    
    Task<bool> UpdateAsync(int id, TUpdateDto updateDto);
    
    Task<bool> DeleteAsync(int id);
    
    Task<bool> DeleteAllAsync();
    
}
