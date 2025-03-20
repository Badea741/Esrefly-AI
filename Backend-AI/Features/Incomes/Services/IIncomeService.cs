
using Esrefly.Features.Shared.Entities;

namespace Esrefly.Features.Incomes.Services;

public interface IIncomeService
{
    Task<List<Income>> GetAllIncomesAsync();
    Task<List<Income>> GetUserIncomesAsync(Guid userId);
    Task<Income?> GetIncomeByIdAsync(Guid id);
    Task<Income> CreateIncomeAsync(Income income, Guid userId);
    Task<bool> UpdateIncomeAsync(Income income);
    Task<bool> DeleteIncomeAsync(Guid id);
}