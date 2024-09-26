namespace web_api_dakota.Models.AI;

public record AiUpdateDTO(
    
    int Id,

    int OrganizationId,
    
    string? Name,

    byte[]? Logo
);