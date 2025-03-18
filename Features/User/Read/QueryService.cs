
using Dapper;
using Npgsql;

namespace Esrefly.Features.User.Read;

public class QueryService(NpgsqlConnection connection) : IQueryService
{
    private static string _basePath = AppDomain.CurrentDomain.BaseDirectory;
    public async Task<UserDto?> GetUserFinancialData(Query query, CancellationToken cancellationToken)
    {
        try
        {
            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync(cancellationToken);

            var sqlQuery =
                await File.ReadAllTextAsync(Path.Combine(_basePath, "Features", "User", "Read", "Query.sql"), cancellationToken);

            var userDictionary = new Dictionary<Guid, UserDto>();
            var result = connection.Query<UserDto, ExpenseDto, IncomeDto, GoalDto, UserDto>(sqlQuery,
                (user, expense, income, goal) =>
                {
                    if (userDictionary.TryGetValue(user.Id, out var userEntry))
                        user = userEntry;
                    else
                        userDictionary.Add(user.Id, user);

                    if (expense != null && !string.IsNullOrEmpty(expense.Description))
                        user.Expenses.Add(expense);

                    if (income != null && !string.IsNullOrEmpty(income.Description))
                        user.Incomes.Add(income);

                    if (goal != null && !string.IsNullOrEmpty(goal.Title))
                        user.Goals.Add(goal);

                    return user;
                },
                new { query.ExternalId }, splitOn: "Description,Description,Title");
            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
