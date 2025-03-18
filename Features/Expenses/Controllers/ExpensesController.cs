
using Esrefly.Features.Expenses.DTOs;
using Esrefly.Features.Expenses.Services;
using Esrefly.Features.Shared.Controllers;
using Esrefly.Features.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using Newtonsoft.Json;

namespace Esrefly.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController(IExpenseService expenseService, IChatClient chatClient) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<List<ExpenseDto>>> GetAllExpenses()
    {
        var expenses = await expenseService.GetAllExpensesAsync();
        var expenseDtos = expenses.Select(e => new ExpenseDto
        {
            Id = e.Id,
            Description = e.Description,
            Amount = e.Amount,
            Category = e.Category
        }).ToList();

        return Ok(expenseDtos);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<ExpenseDto>>> GetUserExpenses(Guid userId)
    {
        var expenses = await expenseService.GetUserExpensesAsync(userId);
        var expenseDtos = expenses.Select(e => new ExpenseDto
        {
            Id = e.Id,
            Description = e.Description,
            Amount = e.Amount,
            Category = e.Category
        }).ToList();

        return Ok(expenseDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseDto>> GetExpense(Guid id)
    {
        var expense = await expenseService.GetExpenseByIdAsync(id);

        if (expense == null)
            return NotFound();

        var expenseDto = new ExpenseDto
        {
            Id = expense.Id,
            Description = expense.Description,
            Amount = expense.Amount,
            Category = expense.Category
        };

        return Ok(expenseDto);
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseDto>> CreateExpense(CreateExpenseDto createExpenseDto)
    {
        try
        {
            var expense = new Expense
            {
                Id = Guid.NewGuid(),
                Description = createExpenseDto.Description,
                Amount = createExpenseDto.Amount,
                Category = createExpenseDto.Category
            };

            if (expense.Category is null)
            {
                ChatMessage[] categoryChatMessage =
                    [new ChatMessage(ChatRole.System,$"Give a general, " +
                    $"concise categorization to the following expense: {expense.Description}"),
                new ChatMessage(ChatRole.Assistant,$"use the following categories: " +$"Food, Housing, Utilities, " +
                    $"Home Appliances, Transportation, Healthcare, Entertainment, Clothing, Education," +
                    $" Insurance, Personal, Debt, Savings, Travel"),
                new ChatMessage(ChatRole.Assistant,$"You must give an answer")];
                var response = await chatClient.GetResponseAsync<ChatCategoryResponse>(categoryChatMessage);
                expense.Category = JsonConvert.DeserializeObject<ChatCategoryResponse>(response.Text).Data.ToString();
            }

            var createdExpense = await expenseService.CreateExpenseAsync(expense, createExpenseDto.UserId);

            var expenseDto = new ExpenseDto
            {
                Id = createdExpense.Id,
                Description = createdExpense.Description,
                Amount = createdExpense.Amount,
                Category = createdExpense.Category
            };

            return CreatedAtAction(nameof(GetExpense), new { id = expenseDto.Id }, expenseDto);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(Guid id, UpdateExpenseDto updateExpenseDto)
    {
        var existingExpense = await expenseService.GetExpenseByIdAsync(id);

        if (existingExpense == null)
            return NotFound();

        existingExpense.Description = updateExpenseDto.Description;
        existingExpense.Amount = updateExpenseDto.Amount;
        existingExpense.Category = updateExpenseDto.Category;

        if (updateExpenseDto.Category is null)
        {
            ChatMessage[] categoryChatMessage =
                    [new ChatMessage(ChatRole.System,$"Give a general, " +
                    $"concise categorization to the following expense: {existingExpense.Description}"),
                new ChatMessage(ChatRole.Assistant,$"use the following categories: " +$"Food, Housing, Utilities, " +
                    $"Home Appliances, Transportation, Healthcare, Entertainment, Clothing, Education," +
                    $" Insurance, Personal, Debt, Savings, Travel"),
                new ChatMessage(ChatRole.Assistant,$"You must give an answer")];
            var response = await chatClient.GetResponseAsync<ChatCategoryResponse>(categoryChatMessage);
            existingExpense.Category = JsonConvert.DeserializeObject<ChatCategoryResponse>(response.Text).Data.ToString();
        }

        var success = await expenseService.UpdateExpenseAsync(existingExpense);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(Guid id)
    {
        var success = await expenseService.DeleteExpenseAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}