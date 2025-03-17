
using Esrefly.Features.Goals.DTOs;
using Esrefly.Features.Goals.Services;
using Esrefly.Features.Shared.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Esrefly.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{
    private readonly IGoalService _goalService;

    public GoalsController(IGoalService goalService)
    {
        _goalService = goalService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GoalDto>>> GetAllGoals()
    {
        var goals = await _goalService.GetAllGoalsAsync();
        var goalDtos = goals.Select(g => new GoalDto
        {
            Id = g.Id,
            Title = g.Title,
            Description = g.Description,
            Amount = g.Amount,
            DeductedRatio = g.DeductedRatio,
            Progress = g.Progress
        }).ToList();

        return Ok(goalDtos);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<GoalDto>>> GetUserGoals(Guid userId)
    {
        var goals = await _goalService.GetUserGoalsAsync(userId);
        var goalDtos = goals.Select(g => new GoalDto
        {
            Id = g.Id,
            Title = g.Title,
            Description = g.Description,
            Amount = g.Amount,
            DeductedRatio = g.DeductedRatio,
            Progress = g.Progress
        }).ToList();

        return Ok(goalDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GoalDto>> GetGoal(Guid id)
    {
        var goal = await _goalService.GetGoalByIdAsync(id);

        if (goal == null)
            return NotFound();

        var goalDto = new GoalDto
        {
            Id = goal.Id,
            Title = goal.Title,
            Description = goal.Description,
            Amount = goal.Amount,
            DeductedRatio = goal.DeductedRatio,
            Progress = goal.Progress
        };

        return Ok(goalDto);
    }

    [HttpPost]
    public async Task<ActionResult<GoalDto>> CreateGoal(CreateGoalDto createGoalDto)
    {
        try
        {
            var goal = new Goal
            {
                Id = Guid.NewGuid(),
                Title = createGoalDto.Title,
                Description = createGoalDto.Description,
                Amount = createGoalDto.Amount,
                DeductedRatio = createGoalDto.DeductedRatio,
                Progress = 0 // Initialize progress to 0
            };

            var createdGoal = await _goalService.CreateGoalAsync(goal, createGoalDto.UserId);

            var goalDto = new GoalDto
            {
                Id = createdGoal.Id,
                Title = createdGoal.Title,
                Description = createdGoal.Description,
                Amount = createdGoal.Amount,
                DeductedRatio = createdGoal.DeductedRatio,
                Progress = createdGoal.Progress
            };

            return CreatedAtAction(nameof(GetGoal), new { id = goalDto.Id }, goalDto);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoal(Guid id, UpdateGoalDto updateGoalDto)
    {
        var existingGoal = await _goalService.GetGoalByIdAsync(id);

        if (existingGoal == null)
            return NotFound();

        existingGoal.Title = updateGoalDto.Title;
        existingGoal.Description = updateGoalDto.Description;
        existingGoal.Amount = updateGoalDto.Amount;
        existingGoal.DeductedRatio = updateGoalDto.DeductedRatio;
        existingGoal.Progress = updateGoalDto.Progress;

        var success = await _goalService.UpdateGoalAsync(existingGoal);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGoal(Guid id)
    {
        var success = await _goalService.DeleteGoalAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}