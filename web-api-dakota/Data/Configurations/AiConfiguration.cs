using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web_api_dakota.Models.AI;

namespace web_api_dakota.Data.AutoMapper;

public class AiConfiguration : IEntityTypeConfiguration<AiModel>
{
    public void Configure(EntityTypeBuilder<AiModel> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
    }
}