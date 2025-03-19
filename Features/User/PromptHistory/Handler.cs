using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Esrefly.Features.User.PromptHistory;

public class Handler(ApplicationDbContext context) : IRequestHandler<Query, IEnumerable<PromptHistoryDto>>
{
    public async Task<IEnumerable<PromptHistoryDto>> Handle(Query request, CancellationToken cancellationToken)
    {
        var userPrompts = await context.Users
            .Include(x => x.Prompts)
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
            ?? throw new Exception("User not found");

        var groupedUserPrompts = userPrompts.Prompts
            .GroupBy(x => DateOnly.FromDateTime(x.CreatedDate.Date));

        var result = groupedUserPrompts.Select(x => new PromptHistoryDto
        {
            CreatedDate = x.Key,
            Prompts = x.Select(x => new PromptDto
            {
                Prompt = x.Value,
                Response = x.Response,
                CreatedDate = x.CreatedDate
            }).ToList()
        });

        return result;
    }
}
