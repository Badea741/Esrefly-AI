namespace Esrefly.Features.Goals.DTOs;

public class CreateGoalDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int DeductedRatio { get; set; }
    public Guid UserId { get; set; }
}