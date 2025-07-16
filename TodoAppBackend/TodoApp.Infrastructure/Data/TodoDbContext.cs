using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Data
{
	public class TodoDbContext : DbContext
	{
		public DbSet<TodoItem> TodoItems { get; set; }

		public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TodoItem>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Title)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.CreatedAt)
					.HasDefaultValueSql("GETUTCDATE()");
			});
		}
	}
}