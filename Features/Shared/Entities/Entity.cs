namespace Esrefly.Features.Shared.Entities;

public class Entity
{
    public Entity()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
}