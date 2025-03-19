using Esrefly.Features.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Esrefly;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Prompt> Prompts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Income>()
            .Property(e => e.TransactionDate)
            .HasConversion(
                dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc),
                dateTime => DateOnly.FromDateTime(dateTime)
            );

        modelBuilder.Entity<Expense>()
            .Property(e => e.TransactionDate)
            .HasConversion(
                dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc),
                dateTime => DateOnly.FromDateTime(dateTime)
            );


    }
}