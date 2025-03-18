namespace Esrefly.Features.Shared.Entities;

public class Entity
{
      public Guid Id { get; init; }
    public DateTimeOffset CreatedDate { get; init; } = DateTimeOffset.UtcNow;
}