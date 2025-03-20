
using Esrefly.Features.Shared.Entities;
using Esrefly.Features.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Esrefly.Features.Expenses.Services;

public class ExpenseService : IExpenseService
{
    private readonly ApplicationDbContext _context;
    private readonly IRepository<Expense> _expenseRepository;

    public ExpenseService(ApplicationDbContext context, IRepository<Expense> expenseRepository)
    {
        _context = context;
        _expenseRepository = expenseRepository;
    }

    public async Task<List<Expense>> GetAllExpensesAsync()
    {
        return await _expenseRepository.GetAllAsync();
    }

    public async Task<List<Expense>> GetUserExpensesAsync(Guid userId)
    {
        return await _context.Expenses
            .Where(e => EF.Property<Guid>(e, "UserId") == userId)
            .ToListAsync();
    }

    public async Task<Expense?> GetExpenseByIdAsync(Guid id)
    {
        return await _expenseRepository.GetByIdAsync(id);
    }

    public async Task<Expense> CreateExpenseAsync(Expense expense, Guid userId)
    {
        // Check if user exists
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {userId} not found");

        // Set the UserId
        _context.Entry(expense).Property("UserId").CurrentValue = userId;

        return await _expenseRepository.AddAsync(expense);
    }

    public async Task<bool> UpdateExpenseAsync(Expense expense)
    {
        var existingExpense = await _expenseRepository.GetByIdAsync(expense.Id);
        if (existingExpense == null)
            return false;

        await _expenseRepository.UpdateAsync(expense);
        return true;
    }

    public async Task<bool> DeleteExpenseAsync(Guid id)
    {
        return await _expenseRepository.DeleteAsync(id);
    }
}