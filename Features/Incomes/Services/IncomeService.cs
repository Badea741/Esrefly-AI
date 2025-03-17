
using Esrefly.Features.Shared.Entities;
using Esrefly.Features.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Esrefly.Features.Incomes.Services;

public class IncomeService : IIncomeService
{
    private readonly ApplicationDbContext _context;
    private readonly IRepository<Income> _incomeRepository;

    public IncomeService(ApplicationDbContext context, IRepository<Income> incomeRepository)
    {
        _context = context;
        _incomeRepository = incomeRepository;
    }

    public async Task<List<Income>> GetAllIncomesAsync()
    {
        return await _incomeRepository.GetAllAsync();
    }

    public async Task<List<Income>> GetUserIncomesAsync(Guid userId)
    {
        return await _context.Incomes
            .Where(i => EF.Property<Guid>(i, "UserId") == userId)
            .ToListAsync();
    }

    public async Task<Income?> GetIncomeByIdAsync(Guid id)
    {
        return await _incomeRepository.GetByIdAsync(id);
    }

    public async Task<Income> CreateIncomeAsync(Income income, Guid userId)
    {
        // Check if user exists
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {userId} not found");

        // Set the UserId
        _context.Entry(income).Property("UserId").CurrentValue = userId;

        return await _incomeRepository.AddAsync(income);
    }

    public async Task<bool> UpdateIncomeAsync(Income income)
    {
        var existingIncome = await _incomeRepository.GetByIdAsync(income.Id);
        if (existingIncome == null)
            return false;

        await _incomeRepository.UpdateAsync(income);
        return true;
    }

    public async Task<bool> DeleteIncomeAsync(Guid id)
    {
        return await _incomeRepository.DeleteAsync(id);
    }
}