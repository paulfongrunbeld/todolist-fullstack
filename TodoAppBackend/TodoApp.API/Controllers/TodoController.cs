using Microsoft.AspNetCore.Mvc;
using TodoApp.Core.Services;
using TodoApp.API.DTOs.Requests;
using TodoApp.API.DTOs.Responses;
using AutoMapper;

namespace TodoApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TodoController : ControllerBase
	{
		private readonly ITodoService _todoService;
		private readonly IMapper _mapper;

		public TodoController(ITodoService todoService, IMapper mapper)
		{
			_todoService = todoService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetAll()
		{
			var items = await _todoService.GetAllItemsAsync();
			return Ok(_mapper.Map<IEnumerable<TodoItemDto>>(items));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TodoItemDto>> GetById(int id)
		{
			var item = await _todoService.GetItemByIdAsync(id);
			if (item == null) return NotFound();
			return Ok(_mapper.Map<TodoItemDto>(item));
		}

		[HttpPost]
		public async Task<ActionResult<TodoItemDto>> Create([FromBody] CreateTodoItemDto dto)
		{
			var item = await _todoService.CreateItemAsync(dto.Title);
			return CreatedAtAction(nameof(GetById),
				new { id = item.Id },
				_mapper.Map<TodoItemDto>(item));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] UpdateTodoItemDto dto)
		{
			await _todoService.UpdateItemAsync(id, dto.Title, dto.IsCompleted);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _todoService.DeleteItemAsync(id);
			return NoContent();
		}
	}
}