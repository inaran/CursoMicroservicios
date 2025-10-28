using Microsoft.AspNetCore.Mvc;
using webapi.Application.Services;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredienteController : ControllerBase
{
    private readonly IIngredienteService _ingredienteService;

    public IngredienteController(IIngredienteService ingredienteService)
    {
        _ingredienteService = ingredienteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetIngredientes()
    {
        var ingredientes = await _ingredienteService.GetAllIngredientesAsync();
        return Ok(ingredientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetIngrediente(int id)
    {
        var ingrediente = await _ingredienteService.GetIngredienteByIdAsync(id);
        if (ingrediente == null)
        {
            return NotFound();
        }
        return Ok(ingrediente);
    }
}
