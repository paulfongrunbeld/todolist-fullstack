namespace TodoApp.Domain.Exceptions
{
	public class TodoItemNotFoundException : TodoDomainException
	{
		public int ItemId { get; }

		public TodoItemNotFoundException(int itemId)
			: base($"Todo item with ID {itemId} was not found.")
		{
			ItemId = itemId;
		}
	}
}