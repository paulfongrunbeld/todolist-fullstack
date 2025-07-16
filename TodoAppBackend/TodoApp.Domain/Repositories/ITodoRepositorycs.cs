using System.Linq.Expressions;
using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Repositories
{
    // Интерфейс для базовых операций CRUD (можно вынести в отдельный интерфейс, если нужно)
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    }

    // Специфичный интерфейс для TodoItem (дополняет IRepository)
    public interface ITodoRepository : IRepository<TodoItem>
    {
        // Можно добавить специфичные методы, например:
        Task<IReadOnlyList<TodoItem>> GetCompletedItemsAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TodoItem>> GetItemsByTitleAsync(string title, CancellationToken cancellationToken = default);
    }
}