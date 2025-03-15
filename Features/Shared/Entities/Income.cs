namespace Esrefly.Features.Shared.Entities;

public class Income : Entity
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public IncomeType IncomeType { get; set; } = IncomeType.Fixed;
}

public enum IncomeType
{
    Recurring,
    Fixed
}
