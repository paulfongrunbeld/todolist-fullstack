using AutoMapper;
using TodoApp.API.DTOs.Requests;
using TodoApp.API.DTOs.Responses;
using TodoApp.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TodoApp.API.Mappings
{
	public class TodoProfile : Profile
	{
		public TodoProfile()
		{
			// Request -> Domain
			CreateMap<CreateTodoItemDto, TodoItem>();
			CreateMap<UpdateTodoItemDto, TodoItem>();

			// Domain -> Response
			CreateMap<TodoItem, TodoItemDto>();
		}
	}
}