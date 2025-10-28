using webapi.Application.DTOs;
using webapi.Application.Services;
using webapi.Domain.Repositories;

namespace webapi.Application.Services;

public class IngredienteService : IIngredienteService
{
    private readonly IIngredienteRepository _ingredienteRepository;

    public IngredienteService(IIngredienteRepository ingredienteRepository)
    {
        _ingredienteRepository = ingredienteRepository;
    }

    public async Task<IEnumerable<IngredienteDto>> GetAllIngredientesAsync()
    {
        var ingredientes = await _ingredienteRepository.GetAllAsync();
        return ingredientes.Select(i => new IngredienteDto
        {
            Id = i.Id,
            Name = i.Name,
            Costo = i.Costo.Amount
        });
    }

    public async Task<IngredienteDto?> GetIngredienteByIdAsync(int id)
    {
        var ingrediente = await _ingredienteRepository.GetByIdAsync(id);
        if (ingrediente == null)
            return null;

        return new IngredienteDto
        {
            Id = ingrediente.Id,
            Name = ingrediente.Name,
            Costo = ingrediente.Costo.Amount
        };
    }
}