using webapi.Application.DTOs;
using webapi.Application.Services;
using webapi.Domain.Entities;
using webapi.Domain.Repositories;

namespace webapi.Application.Services;

public class PizzaService : IPizzaService
{
    private readonly IPizzaRepository _pizzaRepository;
    private readonly IIngredienteRepository _ingredienteRepository;

    public PizzaService(IPizzaRepository pizzaRepository, IIngredienteRepository ingredienteRepository)
    {
        _pizzaRepository = pizzaRepository;
        _ingredienteRepository = ingredienteRepository;
    }

    public async Task<IEnumerable<PizzaDto>> GetAllPizzasAsync()
    {
        var pizzas = await _pizzaRepository.GetAllAsync();
        return pizzas.Select(MapToDto);
    }

    public async Task<PizzaDto?> GetPizzaByIdAsync(int id)
    {
        var pizza = await _pizzaRepository.GetByIdAsync(id);
        return pizza != null ? MapToDto(pizza) : null;
    }

    public async Task<PizzaDto> CreatePizzaAsync(CreatePizzaDto createPizzaDto)
    {
        // Validar que los ingredientes existan
        var ingredientes = await _ingredienteRepository.GetByIdsAsync(createPizzaDto.IngredienteIds);
        var ingredientesList = ingredientes.ToList();
        
        if (ingredientesList.Count != createPizzaDto.IngredienteIds.Count)
        {
            var missingIds = createPizzaDto.IngredienteIds.Except(ingredientesList.Select(i => i.Id));
            throw new ArgumentException($"Los siguientes ingredientes no existen: {string.Join(", ", missingIds)}");
        }

        // Crear la entidad de dominio
        var pizza = Pizza.Create(
            0, // El ID se asignará en el repositorio
            createPizzaDto.Name,
            createPizzaDto.Description,
            createPizzaDto.ImageUrl);

        // Agregar ingredientes
        foreach (var ingrediente in ingredientesList)
        {
            pizza.AddIngrediente(ingrediente);
        }

        // Guardar en el repositorio
        var savedPizza = await _pizzaRepository.AddAsync(pizza);
        return MapToDto(savedPizza);
    }

    public async Task<PizzaDto?> UpdatePizzaAsync(int id, UpdatePizzaDto updatePizzaDto)
    {
        var pizza = await _pizzaRepository.GetByIdAsync(id);
        if (pizza == null)
            return null;

        // Actualizar propiedades básicas
        if (!string.IsNullOrEmpty(updatePizzaDto.Name))
            pizza.UpdateName(updatePizzaDto.Name);

        if (!string.IsNullOrEmpty(updatePizzaDto.Description))
            pizza.UpdateDescription(updatePizzaDto.Description);

        if (!string.IsNullOrEmpty(updatePizzaDto.ImageUrl))
            pizza.UpdateImageUrl(updatePizzaDto.ImageUrl);

        // Actualizar ingredientes si se proporcionan
        if (updatePizzaDto.IngredienteIds != null)
        {
            var ingredientes = await _ingredienteRepository.GetByIdsAsync(updatePizzaDto.IngredienteIds);
            var ingredientesList = ingredientes.ToList();

            if (ingredientesList.Count != updatePizzaDto.IngredienteIds.Count)
            {
                var missingIds = updatePizzaDto.IngredienteIds.Except(ingredientesList.Select(i => i.Id));
                throw new ArgumentException($"Los siguientes ingredientes no existen: {string.Join(", ", missingIds)}");
            }

            // Limpiar ingredientes actuales y agregar los nuevos
            pizza.ClearIngredientes();
            foreach (var ingrediente in ingredientesList)
            {
                pizza.AddIngrediente(ingrediente);
            }
        }

        await _pizzaRepository.UpdateAsync(pizza);
        return MapToDto(pizza);
    }

    public async Task<bool> DeletePizzaAsync(int id)
    {
        if (!await _pizzaRepository.ExistsAsync(id))
            return false;

        await _pizzaRepository.DeleteAsync(id);
        return true;
    }

    private static PizzaDto MapToDto(Pizza pizza)
    {
        return new PizzaDto
        {
            Id = pizza.Id,
            Name = pizza.Name,
            Description = pizza.Description,
            ImageUrl = pizza.ImageUrl.Value,
            Price = pizza.Price.Amount,
            Ingredientes = pizza.Ingredientes.Select(i => new IngredienteDto
            {
                Id = i.Id,
                Name = i.Name,
                Costo = i.Costo.Amount
            }).ToList()
        };
    }
}