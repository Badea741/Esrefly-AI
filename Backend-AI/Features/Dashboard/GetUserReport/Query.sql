WITH balance_calcs AS (
    SELECT 
        usr."Id",
        usr."ExternalId",
        usr."Name",
        SUM(usr."TotalBalance") AS user_balance,
        SUM(COALESCE(income."Amount", 0)) AS total_income,
        SUM(COALESCE(expense."Amount", 0)) AS total_expense,
        COALESCE(SUM(goal."DeductedRatio"), 0) AS deducted_ratio,
        SUM(goal."Amount") AS goal_amount,
        COUNT(goal."Id") AS goal_count
    FROM "Users" usr
    LEFT JOIN "Expenses" expense ON expense."UserId" = usr."Id"
    LEFT JOIN "Incomes" income ON income."UserId" = usr."Id"
    LEFT JOIN "Goals" goal ON goal."UserId" = usr."Id"
    WHERE usr."Id"=@userId
    GROUP BY usr."Id", usr."ExternalId", usr."Name"
)
SELECT 
    "Id",
    "ExternalId",
    "Name",
    ROUND((user_balance + total_income - total_expense)::NUMERIC, 2) AS TotalBalance,
    ROUND(((user_balance + total_income - total_expense) - ((user_balance + total_income - total_expense) * deducted_ratio/100))::NUMERIC, 2) AS ManageableBalance,
    CASE 
        WHEN goal_count = 0 THEN NULL
        WHEN goal_amount = 0 THEN 0
        ELSE LEAST(
            ROUND((((user_balance + total_income - total_expense) * deducted_ratio/100) / NULLIF(goal_amount, 0)) * 100::NUMERIC, 2),
            100
        )
    END AS GoalProgress,
    ROUND(total_income::NUMERIC, 2) AS TotalIncome,
    ROUND(total_expense::NUMERIC, 2) AS TotalExpense
FROM balance_calcs