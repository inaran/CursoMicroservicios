using Microsoft.AspNetCore.Mvc;
using webapi.Application.DTOs;
using webapi.Application.Services;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzaController : ControllerBase
{
    private readonly IPizzaService _pizzaService;

    public PizzaController(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PizzaDto>>> GetPizzas()
    {
        var pizzas = await _pizzaService.GetAllPizzasAsync();
        return Ok(pizzas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PizzaDto>> GetPizza(int id)
    {
        var pizza = await _pizzaService.GetPizzaByIdAsync(id);
        if (pizza == null)
        {
            return NotFound();
        }
        return Ok(pizza);
    }

    [HttpPost]
    public async Task<ActionResult<PizzaDto>> CreatePizza(CreatePizzaDto createPizzaDto)
    {
        try
        {
            var pizza = await _pizzaService.CreatePizzaAsync(createPizzaDto);
            return CreatedAtAction(nameof(GetPizza), new { id = pizza.Id }, pizza);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PizzaDto>> UpdatePizza(int id, UpdatePizzaDto updatePizzaDto)
    {
        try
        {
            var pizza = await _pizzaService.UpdatePizzaAsync(id, updatePizzaDto);
            if (pizza == null)
            {
                return NotFound();
            }
            return Ok(pizza);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePizza(int id)
    {
        var deleted = await _pizzaService.DeletePizzaAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

}