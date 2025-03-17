
using Esrefly.Features.Shared.Entities;
using Esrefly.Features.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Esrefly.Features.Goals.Services;

public class GoalService : IGoalService
{
    private readonly ApplicationDbContext _context;
    private readonly IRepository<Goal> _goalRepository;

    public GoalService(ApplicationDbContext context, IRepository<Goal> goalRepository)
    {
        _context = context;
        _goalRepository = goalRepository;
    }

    public async Task<List<Goal>> GetAllGoalsAsync()
    {
        return await _goalRepository.GetAllAsync();
    }

    public async Task<List<Goal>> GetUserGoalsAsync(Guid userId)
    {
        return await _context.Goals
            .Where(g => EF.Property<Guid>(g, "UserId") == userId)
            .ToListAsync();
    }

    public async Task<Goal?> GetGoalByIdAsync(Guid id)
    {
        return await _goalRepository.GetByIdAsync(id);
    }

    public async Task<Goal> CreateGoalAsync(Goal goal, Guid userId)
    {
        // Check if user exists
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {userId} not found");

        // Set the UserId
        _context.Entry(goal).Property("UserId").CurrentValue = userId;

        return await _goalRepository.AddAsync(goal);
    }

    public async Task<bool> UpdateGoalAsync(Goal goal)
    {
        var existingGoal = await _goalRepository.GetByIdAsync(goal.Id);
        if (existingGoal == null)
            return false;

        await _goalRepository.UpdateAsync(goal);
        return true;
    }

    public async Task<bool> DeleteGoalAsync(Guid id)
    {
        return await _goalRepository.DeleteAsync(id);
    }
}