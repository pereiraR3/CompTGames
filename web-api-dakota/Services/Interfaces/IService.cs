namespace web_api_dakota.Services.Interfaces;

public interface IService<T, TRequestDto, TResponseDto, TUpdateDto> where T : class
{
    Task<IEnumerable<TResponseDto>> GetAllAsync();
    
    Task<TResponseDto?> GetByIdAsync(int id);
    
    Task<TResponseDto> AddAsync(TRequestDto requestDto); 
    
    Task<bool> UpdateAsync(int Id, TUpdateDto updateDto);
    
    Task<bool> DeleteAsync(int id);
    
    Task<bool> DeleteAllAsync();
    
}
