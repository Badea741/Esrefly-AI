namespace Esrefly.Features.Incomes.DTOs;

public class IncomeDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string IncomeType { get; set; } = string.Empty; 
    public DateTimeOffset CreatedDate { get; set; } 
    public DateOnly TransactionDate { get; set; } 
}