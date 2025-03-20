
using Esrefly.Features.Incomes.DTOs;
using Esrefly.Features.Incomes.Services;
using Esrefly.Features.Shared.Controllers;
using Esrefly.Features.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Esrefly.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomesController : BaseController
{
    private readonly IIncomeService _incomeService;

    public IncomesController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<IncomeDto>>> GetAllIncomes()
    {
        var incomes = await _incomeService.GetAllIncomesAsync();
        var incomeDtos = incomes.Select(i => new IncomeDto
        {
            Id = i.Id,
            Description = i.Description,
            Amount = i.Amount,
            IncomeType = i.IncomeType,
            CreatedDate = i.CreatedDate,
            TransactionDate = i.TransactionDate
        }).ToList();

        return Ok(incomeDtos);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<IncomeDto>>> GetUserIncomes(Guid userId)
    {
        var incomes = await _incomeService.GetUserIncomesAsync(userId);
        var incomeDtos = incomes.Select(i => new IncomeDto
        {
            Id = i.Id,
            Description = i.Description,
            Amount = i.Amount,
            IncomeType = i.IncomeType,
            CreatedDate = i.CreatedDate,
            TransactionDate = i.TransactionDate
        }).ToList();

        return Ok(incomeDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IncomeDto>> GetIncome(Guid id)
    {
        var income = await _incomeService.GetIncomeByIdAsync(id);

        if (income == null)
            return NotFound();

        var incomeDto = new IncomeDto
        {
            Id = income.Id,
            Description = income.Description,
            Amount = income.Amount,
            IncomeType = income.IncomeType,
            CreatedDate = income.CreatedDate,
            TransactionDate = income.TransactionDate
        };

        return Ok(incomeDto);
    }

    [HttpPost]
    public async Task<ActionResult<IncomeDto>> CreateIncome(CreateIncomeDto createIncomeDto)
    {
        try
        {
            var income = new Income
            {
                Id = Guid.NewGuid(),
                Description = createIncomeDto.Description,
                Amount = createIncomeDto.Amount,
                IncomeType = createIncomeDto.IncomeType,
                TransactionDate = createIncomeDto.TransactionDate
            };

            var createdIncome = await _incomeService.CreateIncomeAsync(income, createIncomeDto.UserId);

            var incomeDto = new IncomeDto
            {
                Id = createdIncome.Id,
                Description = createdIncome.Description,
                Amount = createdIncome.Amount,
                IncomeType = createdIncome.IncomeType,
                CreatedDate = createdIncome.CreatedDate,
                TransactionDate = createdIncome.TransactionDate
            };

            return CreatedAtAction(nameof(GetIncome), new { id = incomeDto.Id }, incomeDto);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIncome(Guid id, UpdateIncomeDto updateIncomeDto)
    {
        var existingIncome = await _incomeService.GetIncomeByIdAsync(id);

        if (existingIncome == null)
            return NotFound();

       
        existingIncome.Description = updateIncomeDto.Description;
        existingIncome.Amount = updateIncomeDto.Amount;
        existingIncome.IncomeType = updateIncomeDto.IncomeType;
        existingIncome.TransactionDate = updateIncomeDto.TransactionDate;

       
        var success = await _incomeService.UpdateIncomeAsync(existingIncome);

        if (!success)
            return NotFound();

       
        return Ok(existingIncome);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIncome(Guid id)
    {
        var success = await _incomeService.DeleteIncomeAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}