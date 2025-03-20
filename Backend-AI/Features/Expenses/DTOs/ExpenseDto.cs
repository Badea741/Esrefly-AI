namespace Esrefly.Features.Expenses.DTOs;

public class ExpenseDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Category { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateOnly TransactionDate { get; set; } 
}