using TodoApp.Domain.Entities;
using TodoApp.Domain.Exceptions;
using TodoApp.Domain.Repositories;

namespace TodoApp.Core.Services
{
	public class TodoService : ITodoService
	{
		private readonly ITodoRepository _repository;

		// Инъекция зависимости репозитория
		public TodoService(ITodoRepository repository)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		public async Task<IReadOnlyList<TodoItem>> GetAllItemsAsync(CancellationToken cancellationToken = default)
		{
			return (await _repository.GetAllAsync(cancellationToken)).ToList();
		}

		public async Task<TodoItem?> GetItemByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			return await _repository.GetByIdAsync(id, cancellationToken);
		}

		public async Task<TodoItem> CreateItemAsync(string title, CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(title))
				throw new ArgumentException("Title cannot be empty.", nameof(title));

			var item = new TodoItem(title);
			await _repository.AddAsync(item, cancellationToken);
			return item;
		}

		public async Task UpdateItemAsync(int id, string title, bool isCompleted, CancellationToken cancellationToken = default)
		{
			var item = await _repository.GetByIdAsync(id, cancellationToken);
			if (item == null)
				throw new TodoItemNotFoundException(id);

			item.UpdateTitle(title);

			if (isCompleted && !item.IsCompleted)
				item.MarkAsCompleted();
			else if (!isCompleted && item.IsCompleted)
				item.MarkAsIncomplete();

			await _repository.UpdateAsync(item, cancellationToken);
		}

		public async Task DeleteItemAsync(int id, CancellationToken cancellationToken = default)
		{
			var item = await _repository.GetByIdAsync(id, cancellationToken);
			if (item == null)
				throw new TodoItemNotFoundException(id);

			await _repository.DeleteAsync(item, cancellationToken);
		}

		public async Task MarkAsCompletedAsync(int id, CancellationToken cancellationToken = default)
		{
			var item = await _repository.GetByIdAsync(id, cancellationToken);
			if (item == null)
				throw new TodoItemNotFoundException(id);

			item.MarkAsCompleted();
			await _repository.UpdateAsync(item, cancellationToken);
		}

		public async Task MarkAsIncompleteAsync(int id, CancellationToken cancellationToken = default)
		{
			var item = await _repository.GetByIdAsync(id, cancellationToken);
			if (item == null)
				throw new TodoItemNotFoundException(id);

			item.MarkAsIncomplete();
			await _repository.UpdateAsync(item, cancellationToken);
		}
	}
}