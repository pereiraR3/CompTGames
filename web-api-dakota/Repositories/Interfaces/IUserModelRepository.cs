namespace web_api_dakota.Repositories.Interfaces;

public interface IUserModelRepository
{
    
    Task<bool> AddRoleToUserModelAsync(int userModelId, int roleModelId);
    
    Task<bool> RemoveRoleFromUserModelAsync(int userModelId, int roleModelId);
    
}