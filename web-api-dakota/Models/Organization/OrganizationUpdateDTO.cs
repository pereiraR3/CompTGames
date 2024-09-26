namespace web_api_dakota.Models.Organization;

public record OrganizationUpdateDTO(
    
    string Name, 

    string Website,

    byte[] Logo
    
);