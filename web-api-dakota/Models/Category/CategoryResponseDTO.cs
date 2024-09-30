using web_api_dakota.Models.AI;

namespace web_api_dakota.Models.Category;

public record CategoryResponseDTO
(
    int Id,

    String Name,

    String Description,

    List<AiResponseDTO>? AiModels
    
);

