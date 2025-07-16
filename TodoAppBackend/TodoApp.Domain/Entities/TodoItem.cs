namespace TodoApp.Domain.Entities
{
	public class TodoItem
	{
		// Идентификатор (Primary Key)
		public int Id { get; private set; }

		// Название задачи (обязательное поле)
		public string Title { get; private set; }

		// Статус выполнения
		public bool IsCompleted { get; private set; }

		// Дата создания (устанавливается автоматически)
		public DateTime CreatedAt { get; private set; }

		// Дата завершения (nullable, так как задача может быть не завершена)
		public DateTime? CompletedAt { get; private set; }

		// Приватный конструктор для EF Core (если используется)
		private TodoItem() { }

		// Основной конструктор
		public TodoItem(string title)
		{
			if (string.IsNullOrWhiteSpace(title))
				throw new ArgumentException("Title cannot be empty or whitespace.", nameof(title));

			Title = title.Trim();
			IsCompleted = false;
			CreatedAt = DateTime.UtcNow;
			CompletedAt = null;
		}

		// Методы для изменения состояния (инкапсуляция логики)
		public void MarkAsCompleted()
		{
			if (!IsCompleted)
			{
				IsCompleted = true;
				CompletedAt = DateTime.UtcNow;
			}
		}

		public void MarkAsIncomplete()
		{
			if (IsCompleted)
			{
				IsCompleted = false;
				CompletedAt = null;
			}
		}

		public void UpdateTitle(string newTitle)
		{
			if (string.IsNullOrWhiteSpace(newTitle))
				throw new ArgumentException("Title cannot be empty or whitespace.", nameof(newTitle));

			Title = newTitle.Trim();
		}
	}
}