namespace web_api_dakota.Services.Interfaces;

public interface IUserModelService
{
    
    Task<bool> AddRoleToUserModelAsync(int userModelId, int roleModelId);
    
    Task<bool> RemoveRoleFromUserModelAsync(int userModelId, int roleModelId);
    
}