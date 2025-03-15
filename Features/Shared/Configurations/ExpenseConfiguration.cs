using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Esrefly.Features.Shared.Configurations;

public class ExpenseConfiguration : IEntityTypeConfiguration<Entities.Expense>
{
    public void Configure(EntityTypeBuilder<Entities.Expense> builder)
    {
        builder.HasKey(x => x.Id);
    }
}