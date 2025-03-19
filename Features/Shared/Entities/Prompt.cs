namespace Esrefly.Features.Shared.Entities;

public class Prompt : Entity
{
    public Prompt(string value, string response)
    {
        Value = value;
        Response = response;
    }

    public string Value { get; init; } = string.Empty;
    public string Response { get; init; } = string.Empty;
    public User User { get; set; } = default!;
    public Guid UserId { get; set; }
}