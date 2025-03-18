using MediatR;

namespace Esrefly.Features.Dashboard.GetUserReport;

public record Query(Guid UserId) : IRequest<UserReportDto>;