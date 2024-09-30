using AutoMapper;
using web_api_dakota.Repositories.Interfaces;
using web_api_dakota.Services.Interfaces;

namespace web_api_dakota.Services;

public class UserModelService : IUserModelService
{
    
    private readonly IUserModelRepository _userModelRepository;
    private readonly IMapper _mapper;
    
    public UserModelService(IUserModelRepository userModelRepository, IMapper mapper)
    {
        _userModelRepository = userModelRepository;
        _mapper = mapper;
    }
    
    public async Task<bool> AddRoleToUserModelAsync(int userModelId, int roleModelId)
    {
        return await _userModelRepository.AddRoleToUserModelAsync(userModelId, roleModelId);
    }

    public async Task<bool> RemoveRoleFromUserModelAsync(int userModelId, int roleModelId)
    {
        return await _userModelRepository.RemoveRoleFromUserModelAsync(userModelId, roleModelId);
    }
}