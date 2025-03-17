
using Esrefly.Features.Expenses.DTOs;
using Esrefly.Features.Expenses.Services;
using Esrefly.Features.Shared.Controllers;
using Esrefly.Features.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Esrefly.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : BaseController
{
    private readonly IExpenseService _expenseService;

    public ExpensesController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ExpenseDto>>> GetAllExpenses()
    {
        var expenses = await _expenseService.GetAllExpensesAsync();
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
        var expenses = await _expenseService.GetUserExpensesAsync(userId);
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
        var expense = await _expenseService.GetExpenseByIdAsync(id);

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

            var createdExpense = await _expenseService.CreateExpenseAsync(expense, createExpenseDto.UserId);

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
        var existingExpense = await _expenseService.GetExpenseByIdAsync(id);

        if (existingExpense == null)
            return NotFound();

        existingExpense.Description = updateExpenseDto.Description;
        existingExpense.Amount = updateExpenseDto.Amount;
        existingExpense.Category = updateExpenseDto.Category;

        var success = await _expenseService.UpdateExpenseAsync(existingExpense);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(Guid id)
    {
        var success = await _expenseService.DeleteExpenseAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}