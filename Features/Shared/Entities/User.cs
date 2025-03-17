namespace Esrefly.Features.Shared.Entities;

public class User : Entity
{
    public User(string externalId, string name, decimal totalBalance = 0)
    {
        ExternalId = externalId;
        Name = name;
        TotalBalance = totalBalance;
    }

    public string ExternalId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public decimal TotalBalance { get; init; }

    public ICollection<Income> Incomes { get; set; } = [];
    public ICollection<Expense> Expenses { get; set; } = [];
    public ICollection<Goal> Goals { get; set; } = [];
}