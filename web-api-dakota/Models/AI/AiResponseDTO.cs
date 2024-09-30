
using web_api_dakota.Models.Category;
using web_api_dakota.Models.Organization;

namespace web_api_dakota.Models.AI;
public class AiResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte[] Logo { get; set; }

    public OrganizationResponseDTO? Organization { get; set; }
    public ICollection<CategoryResponseDTO>? Categories { get; set; } = new List<CategoryResponseDTO>();
    // Construtor sem parâmetros
    public AiResponseDTO() { }

    // Construtor adicional se necessário
    public AiResponseDTO(int id, string name, byte[] logo, OrganizationResponseDTO organization, ICollection<CategoryResponseDTO> categories)
    {
        Id = id;
        Name = name;
        Logo = logo;
        Organization = organization;
        Categories = categories;
    }
}
