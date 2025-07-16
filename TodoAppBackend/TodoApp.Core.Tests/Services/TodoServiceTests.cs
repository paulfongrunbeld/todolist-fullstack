using Moq;
using TodoApp.Core.Services;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Repositories;
using FluentAssertions;
using TodoApp.Domain.Exceptions;

namespace TodoApp.Core.Tests.Services
{
	public class TodoServiceTests
	{
		private readonly Mock<ITodoRepository> _mockRepo;
		private readonly TodoService _service;

		public TodoServiceTests()
		{
			_mockRepo = new Mock<ITodoRepository>();
			_service = new TodoService(_mockRepo.Object);
		}

		[Fact]
		public async Task CreateItemAsync_ValidTitle_ReturnsItem()
		{
			// Arrange
			var title = "Test Task";
			_mockRepo.Setup(r => r.AddAsync(It.IsAny<TodoItem>(), It.IsAny<CancellationToken>()))
					 .Returns(Task.CompletedTask);

			// Act
			var result = await _service.CreateItemAsync(title);

			// Assert
			result.Title.Should().Be(title);
			result.IsCompleted.Should().BeFalse();
			_mockRepo.Verify(r => r.AddAsync(It.IsAny<TodoItem>(), It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task CreateItemAsync_EmptyTitle_ThrowsArgumentException()
		{
			// Act & Assert
			await Assert.ThrowsAsync<ArgumentException>(() =>
				_service.CreateItemAsync(""));

			_mockRepo.Verify(r => r.AddAsync(It.IsAny<TodoItem>(), It.IsAny<CancellationToken>()), Times.Never);
		}


		[Fact]
		public async Task DeleteItemAsync_NonExistingItem_ThrowsNotFoundException()
		{
			// Arrange
			int nonExistingId = 999;
			_mockRepo.Setup(r => r.GetByIdAsync(nonExistingId, It.IsAny<CancellationToken>()))
					 .ReturnsAsync((TodoItem?)null);

			// Act & Assert
			await Assert.ThrowsAsync<TodoItemNotFoundException>(() =>
				_service.DeleteItemAsync(nonExistingId));

			_mockRepo.Verify(r => r.DeleteAsync(It.IsAny<TodoItem>(), It.IsAny<CancellationToken>()), Times.Never);
		}
	}
}