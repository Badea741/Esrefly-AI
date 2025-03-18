namespace Esrefly.Features.Dashboard.GetUserReport;
public sealed record UserReportDto
{
    public Guid Id { get; init; }
    public string ExternalId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public decimal TotalBalance { get; init; }
    public decimal ManageableBalance { get; init; }
    public decimal? GoalProgress { get; init; }
    public decimal TotalIncome { get; init; }
    public decimal TotalExpense { get; init; }
}