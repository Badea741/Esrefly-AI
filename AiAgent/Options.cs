namespace Esrefly.AiAgent;

public record Options
{
    public string Endpoint { get; init; } = string.Empty;
    public string ModelName { get; init; } = string.Empty;
}
