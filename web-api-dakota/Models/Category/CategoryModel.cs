using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using web_api_dakota.Models.AI;
using web_api_dakota.Services.Interfaces;

namespace web_api_dakota.Models.Category;

[Table("categories")]
public class CategoryModel : IRelatedEntityHasOne<AiModel>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; private set; } 

    [Required]
    [StringLength(50, ErrorMessage = "O nome não pode exceder 50 caracteres.")]
    public string Name { get; private set; } 

    [Required]
    [StringLength(80, ErrorMessage = "A descrição não pode exceder 80 caracteres.")]
    public string Description { get; private set; }

    public List<AiModel> AiModels { get; private set; } = new List<AiModel>(); 

    public CategoryModel() { }

    public CategoryModel(CategoryRequestDTO category)
    {
        Name = category.Name;
        Description = category.Description;
    }

    public void AddAiModel(AiModel aiModel)
    {
        if (aiModel == null) 
            throw new ArgumentNullException(nameof(aiModel));
        
        AiModels.Add(aiModel);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 50)
        {
            throw new ArgumentException("Nome inválido. Deve ter entre 1 e 50 caracteres.");
        }
        Name = name;
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description) || description.Length > 80)
        {
            throw new ArgumentException("Descrição inválida. Deve ter entre 1 e 80 caracteres.");
        }
        Description = description;
    }
    
    
    public void SetParent(AiModel aiModel)
    {
        if (aiModel == null)
        {
            throw new ArgumentException("AI Model cannot be null.");
        }

        AiModels.Add(aiModel);
    }
    
}