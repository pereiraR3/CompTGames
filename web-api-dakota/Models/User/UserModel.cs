using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using web_api_dakota.Models.Roles;

namespace web_api_dakota.Models.User;

[Table("users")]
public class UserModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; private set; }
    
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, ErrorMessage = "The username cannot exceed 20 characters.")]
    public string Username { get; private set; }

    [JsonIgnore]
    [Required(ErrorMessage = "Password is required")]
    [StringLength(60, ErrorMessage = "The password cannot exceed 60 characters.")]
    [Column("password")]
    public string Password { get; private set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(120, ErrorMessage = "The email cannot exceed 120 characters.")]
    [Column("email")]
    public string Email { get; private set; }
    
    [JsonIgnore]
    public List<RoleModel> RoleModels { get; private set; } = new List<RoleModel>();

    public UserModel() { }
        
    public UserModel(UserRequestDTO request)
    {
            
        this.Username = request.Username;

        this.Password = request.Password;

        this.Email = request.Email;
        
    }
    
    // Método para definir ou alterar o nome de usuário, com validação
    public void SetUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username) || username.Length > 50)
        {
            throw new ArgumentException("Invalid username. It must be between 1 and 50 characters.");
        }
        Username = username;
    }

    // Método para definir ou alterar a senha com validação
    public void SetPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            throw new ArgumentException("Password must be at least 8 characters long.");
        }

        // Aqui você deve usar um hash real, como BCrypt, para armazenar a senha de forma segura
        Password = HashPassword(password);
    }
    
    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || email.Length > 120)
        {
            throw new ArgumentException("Invalid email. It must be between 1 and 120 characters.");
        }
        Email = email;
    }

    public void AddRole(RoleModel role)
    {
        if (role == null)
        {
            throw new ArgumentException("Role cannot be null.");
        }
        RoleModels.Add(role);
    }
    

    // Simulação de hash de senha (você deve usar uma solução como BCrypt ou SHA-256)
    private string HashPassword(string password)
    {
        // Substitua isso por um algoritmo real de hash, como BCrypt
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password)); // Simulação de hash
    }
    
}
