
using web_api_dakota.Models.AI;

namespace web_api_dakota.Models.Organization;

public record  OrganizationResponseDTO
(
    int Id,
        
    string Name, 

    string Website,

    byte[] Logo,
        
    List<AiModel> AiModels
    
);
