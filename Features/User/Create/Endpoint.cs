using Esrefly.Features.Shared.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Esrefly.Features.User.Create;

[Route("api/users")]
public class Endpoint(ISender mediator) : BaseController
{
    [HttpPost]
    public async Task<ActionResult> ExecuteAsync(Request request)
    {
        var result = await mediator.Send(request.ToCommand());
        return Ok(result);
    }
}