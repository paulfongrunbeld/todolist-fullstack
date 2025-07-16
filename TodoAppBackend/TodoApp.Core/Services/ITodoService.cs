using TodoApp.Domain.Entities;

namespace TodoApp.Core.Services
{
	public interface ITodoService
	{
		// Получить все задачи
		Task<IReadOnlyList<TodoItem>> GetAllItemsAsync(CancellationToken cancellationToken = default);

		// Получить задачу по ID
		Task<TodoItem?> GetItemByIdAsync(int id, CancellationToken cancellationToken = default);

		// Создать новую задачу
		Task<TodoItem> CreateItemAsync(string title, CancellationToken cancellationToken = default);

		// Обновить задачу
		Task UpdateItemAsync(int id, string title, bool isCompleted, CancellationToken cancellationToken = default);

		// Удалить задачу
		Task DeleteItemAsync(int id, CancellationToken cancellationToken = default);

		// Отметить задачу как выполненную
		Task MarkAsCompletedAsync(int id, CancellationToken cancellationToken = default);

		// Отметить задачу как невыполненную
		Task MarkAsIncompleteAsync(int id, CancellationToken cancellationToken = default);
	}
}