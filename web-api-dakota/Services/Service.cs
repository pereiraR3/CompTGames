using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using web_api_dakota.Models.Category;
using web_api_dakota.Repositories.Interfaces;
using web_api_dakota.Services.Interfaces;

namespace web_api_dakota.Services;

public class Service<T, TRequestDto, TResponseDto, TUpdateDto> : IService<T, TRequestDto, TResponseDto, TUpdateDto> where T : class
{
    private readonly IRepository<T> _repository;
    private readonly IMapper _mapper;
    
    public Service(IRepository<T> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TResponseDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TResponseDto>>(entities);
    }

    public async Task<TResponseDto?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        // Usa o método GetAllIncluding do repositório para aplicar os Includes
        var entity = await _repository.GetByIdIncludingAsync(id, includes);

        // Mapeia a entidade para o DTO de resposta
        return _mapper.Map<TResponseDto>(entity);
    }

    public async Task<TResponseDto> AddAsync(TRequestDto requestDto, object? relatedEntity = null)
    {
        // Mapeia o RequestDTO para a entidade
        var entity = _mapper.Map<T>(requestDto);
    
        // Se a entidade implementa IRelatedEntity<T>, então defina o relacionamento com a entidade pai (ex: Organization)
        if (entity is IRelatedEntityHasOne<object> relatedEntityEntity && relatedEntity != null)
        {
            relatedEntityEntity.SetParent(relatedEntity);
        }
    
        // Adiciona a entidade no banco de dados
        var addedEntity = await _repository.AddAsync(entity);
    
        // Mapeia a entidade criada de volta para o ResponseDTO
        return _mapper.Map<TResponseDto>(addedEntity);
    }


    public async Task<bool> UpdateAsync(int id, TUpdateDto updateDto)
    {
        var entityTarget = await _repository.GetByIdAsync(id); 
        if (entityTarget == null)
        {
            return false; 
        }

        _mapper.Map(updateDto, entityTarget); 
        
        return await _repository.UpdateAsync(entityTarget);
    }
    
    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
    
    public async Task<bool> DeleteAllAsync()
    {
        return await _repository.DeleteAllAsync();
    }
    
}


