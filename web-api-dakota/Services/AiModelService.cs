using AutoMapper;
using web_api_dakota.Models.Organization;
using web_api_dakota.Repositories.Interfaces;
using web_api_dakota.Services.Interfaces;

namespace web_api_dakota.Services;

public class AiModelService : IAiModelService
{
    
    private readonly IAiModelRepository _aiModelRepository;
    private readonly IMapper _mapper;
    
    public AiModelService(IAiModelRepository aiModelRepository, IMapper mapper)
    {
        _aiModelRepository = aiModelRepository;
        _mapper = mapper;
    }

    public async Task<OrganizationResponseDTO> GetOrganizationModelByIdAsync(int id)
    {
        var organizationModel = _aiModelRepository.GetOrganizationModelByIdAsync(id);
        
        return _mapper.Map<OrganizationResponseDTO>(organizationModel);
        
    }

    public async Task<bool> AddCategoryToAiModelAsync(int aiModelId, int categoryId)
    {
        return await _aiModelRepository.AddCategoryToAiModelAsync(aiModelId, categoryId);
    }

    public async Task<bool> RemoveCategoryFromAiModelAsync(int aiModelId, int categoryId)
    {
        return await _aiModelRepository.RemoveCategoryFromAiModelAsync(aiModelId, categoryId);
    }
    
}