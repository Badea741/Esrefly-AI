namespace Esrefly.Features.Shared.Entities;

public class Income : Entity
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public IncomeType IncomeType { get; set; } = IncomeType.Fixed;
    public User User { get; set; } = default!;
    public Guid UserId { get; set; }

}

public enum IncomeType
{
    Recurring,
    Fixed
}
