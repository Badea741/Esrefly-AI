namespace Esrefly.Features.User.Read;

public interface IQueryService
{
    Task<UserDto?> GetUserFinancialData(Query query, CancellationToken cancellationToken);
}