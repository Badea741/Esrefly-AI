namespace Esrefly.Features.Shared.Entities;

public class Goal : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int DeductedRatio { get; set; }
    public int Progress { get; set; }
    public User User { get; set; } = default!;
    public Guid UserId { get; set; }

}