namespace Esrefly.Features.Incomes.DTOs;

public class UpdateIncomeDto
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string IncomeType { get; set; } = string.Empty; 
    public DateOnly TransactionDate { get; set; }
}