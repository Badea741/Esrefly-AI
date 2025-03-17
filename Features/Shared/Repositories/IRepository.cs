
using Esrefly.Features.Shared.Entities;

namespace Esrefly.Features.Shared.Repositories;

public interface IRepository<T> where T : Entity
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
}