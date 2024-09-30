using Microsoft.EntityFrameworkCore;
using web_api_dakota.Data;
using web_api_dakota.Repositories.Interfaces;

namespace web_api_dakota.Repositories;

public class UserModelRepository : IUserModelRepository
{
    
    private readonly WebDakotaContext _dbContext;

    public UserModelRepository(WebDakotaContext context)
    {
        _dbContext = context;
    }
    
    public async Task<bool> AddRoleToUserModelAsync(int userModelId, int roleModelId)
    {
        var userModel = _dbContext.UserModels
            .Include(user => user.RoleModels) // Include the roles related
            .FirstOrDefault(user => user.Id == userModelId);
        
        var role = await _dbContext.RoleModels.FindAsync(roleModelId);
        
        if(role == null || userModel == null)
            return false;

        if (!userModel.RoleModels.Contains(role)) // Verify if the role is not associate
        {
            userModel.RoleModels.Add(role);
            await _dbContext.SaveChangesAsync();
        }

        return true;

    }

    public async Task<bool> RemoveRoleFromUserModelAsync(int userModelId, int roleModelId)
    {
        var userModel = _dbContext.UserModels
            .Include(user => user.RoleModels) // Include the roles related
            .FirstOrDefault(user => user.Id == userModelId);
        
        var role = await _dbContext.RoleModels.FindAsync(roleModelId);
        
        if(role == null || userModel == null)
            return false;

        if (userModel.RoleModels.Contains(role)) // Verify if the role is associate
        {
            userModel.RoleModels.Remove(role);
            await _dbContext.SaveChangesAsync();
        }

        return true;
        
    }
}