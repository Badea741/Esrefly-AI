namespace Esrefly.Features.Expenses.DTOs;

public class CreateExpenseDto
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Category { get; set; }
    public Guid UserId { get; set; }
}