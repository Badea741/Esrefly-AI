using MediatR;

namespace Esrefly.Features.User.Create;

public sealed record Command(string Id, string Nama, decimal? Balance) : IRequest<Guid>
{
}