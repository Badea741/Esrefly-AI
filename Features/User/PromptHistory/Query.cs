using MediatR;

namespace Esrefly.Features.User.PromptHistory;

public record Query(Guid UserId) : IRequest<IEnumerable<PromptHistoryDto>>;