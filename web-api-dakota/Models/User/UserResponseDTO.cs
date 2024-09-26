
namespace web_api_dakota.Models.User;

public record UserResponseDTO
(
        
    int Id,
    
    string Username,

    string Password,
    
    string Email,

    List<UserRole> Roles
    
);
