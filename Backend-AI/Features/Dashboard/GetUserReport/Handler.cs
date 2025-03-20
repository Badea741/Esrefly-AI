using Dapper;
using MediatR;
using Npgsql;

namespace Esrefly.Features.Dashboard.GetUserReport;

public class Handler(NpgsqlConnection connection) : IRequestHandler<Query, UserReportDto>
{
    private static readonly string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    public async Task<UserReportDto> Handle(Query request, CancellationToken cancellationToken)
    {
        if (connection.State != System.Data.ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        var sqlQuery =
            await File.ReadAllTextAsync(Path.Combine(_baseDirectory,
            "Features", "Dashboard", "GetUserReport", "Query.sql"), cancellationToken);

        var result = await connection.QueryFirstOrDefaultAsync<UserReportDto>(
        sqlQuery,
        new { request.UserId }
    ) ?? throw new Exception("user not found");

        return result;
    }
}