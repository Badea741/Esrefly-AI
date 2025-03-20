using Esrefly.Features.Shared.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Esrefly.Features.Dashboard.GetUserReport;

[Route("api/users")]
public class Endpoint(ISender mediator) : BaseController
{
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult> ExecuteAsync(Guid userId)
    {
        var result = await mediator.Send(new Query(userId));
        return Ok(result);
    }
}