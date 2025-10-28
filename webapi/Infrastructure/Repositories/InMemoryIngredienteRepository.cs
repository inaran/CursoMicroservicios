using webapi.Domain.Entities;
using webapi.Domain.Repositories;

namespace webapi.Infrastructure.Repositories;

public class InMemoryIngredienteRepository : IIngredienteRepository
{
    private readonly List<Ingrediente> _ingredientes;
    private int _nextId = 6;

    public InMemoryIngredienteRepository()
    {
        // Datos iniciales
        _ingredientes = new List<Ingrediente>
        {
            Ingrediente.Create(1, "Mozzarella", 2.50m),
            Ingrediente.Create(2, "Tomate", 1.00m),
            Ingrediente.Create(3, "Pepperoni", 3.00m),
            Ingrediente.Create(4, "Champiñones", 1.50m),
            Ingrediente.Create(5, "Aceitunas", 2.00m)
        };
    }

    public Task<Ingrediente?> GetByIdAsync(int id)
    {
        var ingrediente = _ingredientes.FirstOrDefault(i => i.Id == id);
        return Task.FromResult(ingrediente);
    }

    public Task<IEnumerable<Ingrediente>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Ingrediente>>(_ingredientes);
    }

    public Task<IEnumerable<Ingrediente>> GetByIdsAsync(IEnumerable<int> ids)
    {
        var ingredientes = _ingredientes.Where(i => ids.Contains(i.Id));
        return Task.FromResult(ingredientes);
    }

    public Task<Ingrediente> AddAsync(Ingrediente ingrediente)
    {
        var newIngrediente = Ingrediente.Create(_nextId++, ingrediente.Name, ingrediente.Costo.Amount);
        _ingredientes.Add(newIngrediente);
        return Task.FromResult(newIngrediente);
    }

    public Task UpdateAsync(Ingrediente ingrediente)
    {
        var index = _ingredientes.FindIndex(i => i.Id == ingrediente.Id);
        if (index >= 0)
        {
            _ingredientes[index] = ingrediente;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var ingrediente = _ingredientes.FirstOrDefault(i => i.Id == id);
        if (ingrediente != null)
        {
            _ingredientes.Remove(ingrediente);
        }
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(int id)
    {
        var exists = _ingredientes.Any(i => i.Id == id);
        return Task.FromResult(exists);
    }
}