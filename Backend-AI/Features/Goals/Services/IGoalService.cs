
using Esrefly.Features.Shared.Entities;

namespace Esrefly.Features.Goals.Services;

public interface IGoalService
{
    Task<List<Goal>> GetAllGoalsAsync();
    Task<List<Goal>> GetUserGoalsAsync(Guid userId);
    Task<Goal?> GetGoalByIdAsync(Guid id);
    Task<Goal> CreateGoalAsync(Goal goal, Guid userId);
    Task<bool> UpdateGoalAsync(Goal goal);
    Task<bool> DeleteGoalAsync(Guid id);
}