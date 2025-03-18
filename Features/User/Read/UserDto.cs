namespace Esrefly.Features.User.Read;

public record UserDto
{
    public Guid Id { get; init; }
    public string ExternalId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public decimal TotalBalance { get; init; }

    public List<ExpenseDto> Expenses { get; init; } = [];
    public List<IncomeDto> Incomes { get; init; } = [];
    public List<GoalDto> Goals { get; init; } = [];
}

public record ExpenseDto
{
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public string? Category { get; init; }
}

public record IncomeDto
{
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public string IncomeType { get; init; } = string.Empty;
}

public record GoalDto
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public decimal DeductedRatio { get; init; }
    public decimal Progress { get; init; }
}