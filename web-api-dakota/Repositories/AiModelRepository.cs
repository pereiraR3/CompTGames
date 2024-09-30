using Microsoft.EntityFrameworkCore;
using web_api_dakota.Data;
using web_api_dakota.Models.AI;
using web_api_dakota.Models.Organization;
using web_api_dakota.Repositories.Interfaces;

namespace web_api_dakota.Repositories;

public class AiModelRepository : IAiModelRepository
{
    private readonly WebDakotaContext _dbContext;

    public AiModelRepository(WebDakotaContext context)
    {
        _dbContext = context;
    }

    public async Task<OrganizationModel> GetOrganizationModelByIdAsync(int id)
    {
        var organizationModel = await _dbContext.OrganizationModels.FindAsync(id);
        
        if(organizationModel == null)
            return null;
        
        return organizationModel;
    }

    public async Task<bool> AddCategoryToAiModelAsync(int aiModelId, int categoryId)
    {
        var aiModel = await _dbContext.AiModels
            .Include(ai => ai.CategoryModels) // Include the categories related
            .FirstOrDefaultAsync(ai => ai.Id == aiModelId);

        var category = await _dbContext.CategoryModels.FindAsync(categoryId);

        if (aiModel == null || category == null)
            return false;

        if (!aiModel.CategoryModels.Contains(category))  // Verify if the category is not associate
        {
            aiModel.CategoryModels.Add(category);
            await _dbContext.SaveChangesAsync();
        }

        return true;
    }

    public async Task<bool> RemoveCategoryFromAiModelAsync(int aiModelId, int categoryId)
    {
        var aiModel = await _dbContext.AiModels
            .Include(ai => ai.CategoryModels) // Include the categories related
            .FirstOrDefaultAsync(ai => ai.Id == aiModelId);

        var category = await _dbContext.CategoryModels.FindAsync(categoryId);

        if (aiModel == null || category == null)
            return false;

        if (aiModel.CategoryModels.Contains(category)) // Verify if the category is associate
        {
            aiModel.CategoryModels.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        return true;
    }
}