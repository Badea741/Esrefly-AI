using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Esrefly.Features.Shared.Configurations;

public class GoalConfiguration : IEntityTypeConfiguration<Entities.Goal>
{
    public void Configure(EntityTypeBuilder<Entities.Goal> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
