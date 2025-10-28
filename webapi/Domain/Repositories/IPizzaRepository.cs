using webapi.Domain.Entities;

namespace webapi.Domain.Repositories;

public interface IPizzaRepository
{
    Task<Pizza?> GetByIdAsync(int id);
    Task<IEnumerable<Pizza>> GetAllAsync();
    Task<Pizza> AddAsync(Pizza pizza);
    Task UpdateAsync(Pizza pizza);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}