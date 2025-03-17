
using Esrefly.Features.Shared.Entities;

namespace Esrefly.Features.Incomes.DTOs;

public class UpdateIncomeDto
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public IncomeType IncomeType { get; set; }
}