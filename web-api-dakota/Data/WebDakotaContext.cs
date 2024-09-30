using Microsoft.EntityFrameworkCore;
using web_api_dakota.Models.AI;
using web_api_dakota.Models.Category;
using web_api_dakota.Models.Organization;
using web_api_dakota.Models.Plan;
using web_api_dakota.Models.Roles;
using web_api_dakota.Models.User;

namespace web_api_dakota.Data;
public class WebDakotaContext : DbContext
{

    public WebDakotaContext(DbContextOptions<WebDakotaContext> options) : base(options)
    {
        
    }
    
    public DbSet<OrganizationModel> OrganizationModels { get; set; }
    
    public DbSet<UserModel> UserModels { get; set; }
    
    public DbSet<CategoryModel> CategoryModels { get; set; }
    
    public DbSet<PlanModel> PlanModels { get; set; }
    
    public DbSet<AiModel> AiModels { get; set; }
    
    public DbSet<RoleModel> RoleModels { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebDakotaContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
        
    }
    
}

