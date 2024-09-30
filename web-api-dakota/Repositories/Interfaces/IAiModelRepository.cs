using web_api_dakota.Models.Organization;

namespace web_api_dakota.Repositories.Interfaces;

public interface IAiModelRepository
{
    
    Task<OrganizationModel> GetOrganizationModelByIdAsync(int id);
    
    Task<bool> AddCategoryToAiModelAsync(int aiModelId, int categoryId);
    
    Task<bool> RemoveCategoryFromAiModelAsync(int aiModelId, int categoryId);
    
}