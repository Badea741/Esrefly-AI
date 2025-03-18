
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
            var expensesDictionary = new Dictionary<Guid, ExpenseDto>();
            var incomeDictionary = new Dictionary<Guid, IncomeDto>();
            var goalDictionary = new Dictionary<Guid, GoalDto>();

            var result = connection.Query<UserDto, ExpenseDto, IncomeDto, GoalDto, UserDto>(sqlQuery,
                (user, expense, income, goal) =>
                {
                    if (userDictionary.TryGetValue(user.Id, out var userEntry))
                        user = userEntry;
                    else
                        userDictionary.Add(user.Id, user);

                    if (expense != null && !string.IsNullOrEmpty(expense.Description))
                        if (!expensesDictionary.TryGetValue(expense.Id, out var _))
                        {
                            user.Expenses.Add(expense);
                            expensesDictionary.Add(expense.Id, expense);
                        }

                    if (income != null && !string.IsNullOrEmpty(income.Description))
                        if (!incomeDictionary.TryGetValue(income.Id, out var _))
                        {
                            user.Incomes.Add(income);
                            incomeDictionary.Add(income.Id, income);
                        }

                    if (goal != null && !string.IsNullOrEmpty(goal.Title))
                        if (!goalDictionary.TryGetValue(goal.Id, out var _))
                        {
                            user.Goals.Add(goal);
                            goalDictionary.Add(goal.Id, goal);
                        }

                    return user;
                },
                new { query.ExternalId }, splitOn: "Id,Id,Id");
            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
