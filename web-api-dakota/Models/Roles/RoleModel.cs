using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using web_api_dakota.Models.User;

namespace web_api_dakota.Models.Roles;

[Table("roles")]
public class RoleModel
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; private set; }
    
    [Required(ErrorMessage = "Role name is required")]
    [StringLength(20, ErrorMessage = "Role name cannot be longer than 20 characters")]
    [Column("role_name")]
    public string RoleName { get; private set; }
    
    public virtual List<UserModel> UserModels { get; private set; } = new List<UserModel>();
    
    public RoleModel() { }

    public RoleModel(RoleRequestDTO request)
    {
        
        this.RoleName = request.RoleName;
        
    }

    public void SetRoleName(string role)
    {
        if (string.IsNullOrWhiteSpace(role) || role.Length > 20)
        {
            throw new ArgumentException("Invalid role. It must be between 1 and 20 characters.");
        }
        RoleName = role;
    }
    
    public void AddUserModel(UserModel userModel)
    {
        if (userModel == null)
        {
            throw new ArgumentNullException(nameof(userModel), "AI model cannot be null.");
        }

        (UserModels as List<UserModel>)?.Add(userModel);
    }
    
    
    
}