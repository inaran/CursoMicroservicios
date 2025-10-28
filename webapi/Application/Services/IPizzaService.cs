using webapi.Application.DTOs;

namespace webapi.Application.Services;

public interface IPizzaService
{
    Task<IEnumerable<PizzaDto>> GetAllPizzasAsync();
    Task<PizzaDto?> GetPizzaByIdAsync(int id);
    Task<PizzaDto> CreatePizzaAsync(CreatePizzaDto createPizzaDto);
    Task<PizzaDto?> UpdatePizzaAsync(int id, UpdatePizzaDto updatePizzaDto);
    Task<bool> DeletePizzaAsync(int id);
}