namespace Esrefly.Features.User.PromptHistory;

public class PromptHistoryDto
{
    public DateOnly CreatedDate { get; init; }
    public List<PromptDto> Prompts { get; init; } = [];
}

public class PromptDto
{
    public string Prompt { get; init; } = string.Empty;
    public string Response { get; init; } = string.Empty;
    public DateTimeOffset CreatedDate { get; init; }
}