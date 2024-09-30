using web_api_dakota.Models.Organization;

namespace web_api_dakota.Services.Interfaces;

public interface IAiModelService
{
    
    Task<OrganizationResponseDTO> GetOrganizationModelByIdAsync(int id);
    
    Task<bool> AddCategoryToAiModelAsync(int aiModelId, int categoryId);
    
    Task<bool> RemoveCategoryFromAiModelAsync(int aiModelId, int categoryId);

}