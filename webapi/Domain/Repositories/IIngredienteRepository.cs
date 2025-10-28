using webapi.Domain.Entities;

namespace webapi.Domain.Repositories;

public interface IIngredienteRepository
{
    Task<Ingrediente?> GetByIdAsync(int id);
    Task<IEnumerable<Ingrediente>> GetAllAsync();
    Task<IEnumerable<Ingrediente>> GetByIdsAsync(IEnumerable<int> ids);
    Task<Ingrediente> AddAsync(Ingrediente ingrediente);
    Task UpdateAsync(Ingrediente ingrediente);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}