using Esrefly.Features.Shared.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Esrefly.Features.User.PromptHistory;

[Route("api/users")]
public class Endpoint(ISender mediator) : BaseController
{
    [HttpGet("{userId:guid}/prompt-history")]
    public async Task<ActionResult> ExecuteAsync(Guid userId)
    {
        var result = await mediator.Send(new Query(userId));
        return Ok(result);
    }
}