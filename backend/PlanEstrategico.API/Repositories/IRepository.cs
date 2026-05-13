using PlanEstrategico.API.DTOs;
using PlanEstrategico.API.Models;

namespace PlanEstrategico.API.Repositories
{
    /// <summary>
    /// Interfaz base para repositorios
    /// </summary>
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
