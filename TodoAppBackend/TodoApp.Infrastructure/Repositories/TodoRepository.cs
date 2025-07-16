using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Repositories;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure.Repositories
{
	public class TodoRepository : ITodoRepository
	{
		private readonly TodoDbContext _context;

		public TodoRepository(TodoDbContext context)
		{
			_context = context;
		}

		public async Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			return await _context.TodoItems.FindAsync(new object[] { id }, cancellationToken);
		}

		public async Task<IReadOnlyList<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _context.TodoItems.AsNoTracking().ToListAsync(cancellationToken);
		}

		public async Task AddAsync(TodoItem entity, CancellationToken cancellationToken = default)
		{
			await _context.TodoItems.AddAsync(entity, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task UpdateAsync(TodoItem entity, CancellationToken cancellationToken = default)
		{
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task DeleteAsync(TodoItem entity, CancellationToken cancellationToken = default)
		{
			_context.TodoItems.Remove(entity);
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task<bool> ExistsAsync(Expression<Func<TodoItem, bool>> predicate, CancellationToken cancellationToken = default)
		{
			return await _context.TodoItems.AnyAsync(predicate, cancellationToken);
		}

		// Специфичные методы для TodoItem
		public async Task<IReadOnlyList<TodoItem>> GetCompletedItemsAsync(CancellationToken cancellationToken = default)
		{
			return await _context.TodoItems
				.Where(x => x.IsCompleted)
				.AsNoTracking()
				.ToListAsync(cancellationToken);
		}

		public async Task<IReadOnlyList<TodoItem>> GetItemsByTitleAsync(string title, CancellationToken cancellationToken = default)
		{
			return await _context.TodoItems
				.Where(x => x.Title.Contains(title))
				.AsNoTracking()
				.ToListAsync(cancellationToken);
		}
	}
}