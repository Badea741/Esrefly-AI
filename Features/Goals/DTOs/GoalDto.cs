
namespace Esrefly.Features.Goals.DTOs;

public class GoalDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int DeductedRatio { get; set; }
    public int Progress { get; set; }
}