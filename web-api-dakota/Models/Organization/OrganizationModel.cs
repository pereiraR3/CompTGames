
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using web_api_dakota.Models.AI;
using web_api_dakota.Services.Interfaces;

namespace web_api_dakota.Models.Organization;

[Table("organizations")]
public class OrganizationModel
{
        
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; private set; }
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
    [Column("name")]
    public string Name { get; private set; }

    [Required(ErrorMessage = "Address is required")]
    [StringLength(255, ErrorMessage = "Address website cannot be longer than 255 characters")]
    [Column("website")]
    public string Website { get; private set; }

    [Required(ErrorMessage = "Logo is required")]
    [Column("logo")]
    public byte[] Logo { get; private set; }
    
    [JsonIgnore]
    public ICollection<AiModel> AiModels { get; private set; } = new List<AiModel>();
    public OrganizationModel() { }
        
    public OrganizationModel(OrganizationRequestDTO request){

        this.Name = request.Name;
            
        this.Website = request.Website;

        this.Logo = request.Logo;

    }
    
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 50)
        {
            throw new ArgumentException("Invalid name. It must be between 1 and 50 characters.");
        }
        Name = name;
    }

    public void SetWebsite(string website)
    {
        if (string.IsNullOrWhiteSpace(website) || website.Length > 255)
        {
            throw new ArgumentException("Invalid website. It must be between 1 and 255 characters.");
        }
        Website = website;
    }

    public void SetLogo(byte[] logo)
    {
        if (logo == null || logo.Length == 0)
        {
            throw new ArgumentException("Logo cannot be null or empty.");
        }
        Logo = logo;
    }

    public void AddAiModel(AiModel aiModel)
    {
        if (aiModel == null)
        {
            throw new ArgumentNullException(nameof(aiModel), "AI model cannot be null.");
        }

        (AiModels as List<AiModel>)?.Add(aiModel);
    }
    
}
