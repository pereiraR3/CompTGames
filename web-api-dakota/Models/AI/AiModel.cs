using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using web_api_dakota.Models.Category;
using web_api_dakota.Models.Organization;

namespace web_api_dakota.Models.AI;

[Table("ais")]
public class AiModel
{
        
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    
    [Required(ErrorMessage = "The field organizationId is required")]
    [Column("id_organization")]
    public int OrganizationId { get; private set; }
    
    [Required(ErrorMessage = "The name is required")]
    [StringLength(20, ErrorMessage = "The name cannot exceed 20 characters ")]
    public string Name { get; private set; }

    [Required(ErrorMessage = "The logo is required")]
    public byte[] Logo { get; private set; }
    
    [ForeignKey("OrganizationId")]
    public virtual OrganizationModel Organization { get; private set; }
    
    [JsonIgnore]
    public ICollection<CategoryModel> CategoryModels { get; private set; } = new List<CategoryModel>();
    
    public AiModel() { } 
    
    public AiModel(AiRequestDTO request, OrganizationModel organization) {
        
        this.Name = request.Name;
        this.Logo = request.Logo;
        this.OrganizationId = request.OrganizationId;
        this.Organization = organization;

    }
    
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 20)
        {
            throw new ArgumentException("Invalid name. It must be between 1 and 20 characters.");
        }

        Name = name;
    }

    public void SetLogo(byte[] logo)
    {
        if (logo == null || logo.Length == 0)
        {
            throw new ArgumentException("Logo cannot be null or empty.");
        }

        Logo = logo;
    }

    public void SetOrganization(OrganizationModel organization)
    {
        if (organization == null)
        {
            throw new ArgumentException("Organization cannot be null.");
        }

        Organization = organization;
        OrganizationId = organization.Id;
    }

    public void AddCategory(CategoryModel category)
    {
        if(category == null)
            throw new ArgumentNullException(nameof(category), "Category cannot be null.");
        
        (CategoryModels as List<CategoryModel>)?.Add(category);
    }
    
    
}
