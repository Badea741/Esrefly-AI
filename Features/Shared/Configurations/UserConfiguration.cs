﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Esrefly.Features.Shared.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<Entities.User>
{
    public void Configure(EntityTypeBuilder<Entities.User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany<Entities.Income>().WithOne()
            .HasForeignKey("UserId");
        builder.HasMany<Entities.Expense>().WithOne()
            .HasForeignKey("UserId");
        builder.HasMany<Entities.Goal>().WithOne()
            .HasForeignKey("UserId");

        builder.HasIndex(x => x.ExternalId);
    }
}