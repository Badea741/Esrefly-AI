namespace Esrefly.Features.User.Create;

public record Request
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public decimal? TotalBalance { get; init; }

    public Command ToCommand() => new(Id, Name, TotalBalance);
}
