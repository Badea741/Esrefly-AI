namespace Esrefly.Features.Shared.Entities;

public class Income : Entity
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string IncomeType { get; set; } = "Fixed";
    public DateOnly TransactionDate { get; set; }
}

