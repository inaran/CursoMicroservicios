using webapi.Domain.Entities;
using webapi.Domain.Repositories;

namespace webapi.Infrastructure.Repositories;

public class InMemoryPizzaRepository : IPizzaRepository
{
    private readonly List<Pizza> _pizzas = new();
    private int _nextId = 1;

    public Task<Pizza?> GetByIdAsync(int id)
    {
        var pizza = _pizzas.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(pizza);
    }

    public Task<IEnumerable<Pizza>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Pizza>>(_pizzas);
    }

    public Task<Pizza> AddAsync(Pizza pizza)
    {
        var newPizza = Pizza.Create(_nextId++, pizza.Name, pizza.Description, pizza.ImageUrl.Value);
        
        // Agregar los ingredientes
        foreach (var ingrediente in pizza.Ingredientes)
        {
            newPizza.AddIngrediente(ingrediente);
        }

        _pizzas.Add(newPizza);
        return Task.FromResult(newPizza);
    }

    public Task UpdateAsync(Pizza pizza)
    {
        var index = _pizzas.FindIndex(p => p.Id == pizza.Id);
        if (index >= 0)
        {
            _pizzas[index] = pizza;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var pizza = _pizzas.FirstOrDefault(p => p.Id == id);
        if (pizza != null)
        {
            _pizzas.Remove(pizza);
        }
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(int id)
    {
        var exists = _pizzas.Any(p => p.Id == id);
        return Task.FromResult(exists);
    }
}