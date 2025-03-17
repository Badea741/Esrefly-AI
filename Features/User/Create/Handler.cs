using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Esrefly.Features.User.Create;

public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Guid>
{
    public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
    {
        var existingUser = await context.Users.FirstOrDefaultAsync(x => x.ExternalId == request.Id, cancellationToken);

        if (existingUser != null)
            throw new Exception("user with the same id does already exist");

        var user = new Shared.Entities.User(request.Id, request.Nama, request.Balance ?? 0);

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}