
using Esrefly.Features.Shared.Entities;

namespace Esrefly.Features.Expenses.Services;

public interface IExpenseService
{
    Task<List<Expense>> GetAllExpensesAsync();
    Task<List<Expense>> GetUserExpensesAsync(Guid userId);
    Task<Expense?> GetExpenseByIdAsync(Guid id);
    Task<Expense> CreateExpenseAsync(Expense expense, Guid userId);
    Task<bool> UpdateExpenseAsync(Expense expense);
    Task<bool> DeleteExpenseAsync(Guid id);
}