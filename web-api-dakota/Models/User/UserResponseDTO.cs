
using web_api_dakota.Models.Roles;

namespace web_api_dakota.Models.User;

public record UserResponseDTO(

    int Id,

    string Username,

    string Password,

    string Email,

    List<RoleModel>? Roles

)
{
    public UserResponseDTO() : this(0, string.Empty, string.Empty, string.Empty, new List<RoleModel>())
    {
    }
    
}
