namespace web_api_dakota.Models.AI;

public record AiPatchDTO(
    
    int Id,
    
    int? OrganizationId,

    string? Name,

    byte[]? Logo
    
);