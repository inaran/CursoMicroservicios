using webapi.Application.DTOs;

namespace webapi.Application.Services;

public interface IIngredienteService
{
    Task<IEnumerable<IngredienteDto>> GetAllIngredientesAsync();
    Task<IngredienteDto?> GetIngredienteByIdAsync(int id);
}