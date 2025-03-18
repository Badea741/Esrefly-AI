namespace Esrefly.Features.Shared.Entities;

public class Expense : Entity
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Category { get; set; }
    public DateOnly TransactionDate { get; set; } 
}