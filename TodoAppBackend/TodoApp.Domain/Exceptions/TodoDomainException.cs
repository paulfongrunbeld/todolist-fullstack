namespace TodoApp.Domain.Exceptions
{
	public class TodoDomainException : Exception
	{
		public TodoDomainException() { }
		public TodoDomainException(string message) : base(message) { }
		public TodoDomainException(string message, Exception inner) : base(message, inner) { }
	}
}