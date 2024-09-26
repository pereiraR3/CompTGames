using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web_api_dakota.Models.Organization;

namespace web_api_dakota.Data.AutoMapper;

public class OrganizationConfiguration : IEntityTypeConfiguration<OrganizationModel>
{
    public void Configure(EntityTypeBuilder<OrganizationModel> builder)
    {
        
        builder.HasIndex(x => x.Name);
        
        builder.HasIndex(x => x.Website);
        
    }
}