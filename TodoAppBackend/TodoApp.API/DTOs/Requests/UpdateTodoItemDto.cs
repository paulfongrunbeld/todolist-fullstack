namespace TodoApp.API.DTOs.Requests
{
	public class UpdateTodoItemDto
	{
		public string Title { get; set; } = string.Empty;
		public bool IsCompleted { get; set; }
	}
}