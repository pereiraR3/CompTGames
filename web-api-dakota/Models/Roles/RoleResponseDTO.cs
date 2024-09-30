namespace web_api_dakota.Models.Roles;

public record RoleResponseDTO(
    
    int Id,
    
    string RoleName,
    
    List<RoleModel> Roles

);