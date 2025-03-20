namespace Esrefly.Features.Incomes.DTOs;

public class CreateIncomeDto
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string IncomeType { get; set; } = "Fixed"; 
    public Guid UserId { get; set; }
    public DateOnly TransactionDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow); 
}