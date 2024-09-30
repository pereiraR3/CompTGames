using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web_api_dakota.Models.Roles;

namespace web_api_dakota.Data.AutoMapper;

public class RoleConfiguration : IEntityTypeConfiguration<RoleModel>
{
    public void Configure(EntityTypeBuilder<RoleModel> builder)
    {
        builder.HasIndex(x => x.RoleName).IsUnique();
    }
}