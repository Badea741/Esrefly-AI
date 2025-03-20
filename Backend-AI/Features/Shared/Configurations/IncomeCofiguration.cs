using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Esrefly.Features.Shared.Configurations;

public class IncomeCofiguration : IEntityTypeConfiguration<Entities.Income>
{
    public void Configure(EntityTypeBuilder<Entities.Income> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User).WithMany(x => x.Incomes)
            .HasForeignKey(x => x.UserId);
    }
}