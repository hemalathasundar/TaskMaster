using Moq;
using TaskMaster.Application.Commands;
using TaskMaster.Core.Entities;
using TaskMaster.Core.Interfaces;

namespace TaskMaster.Tests.Application.Commands
{
    public class CommandTests
    {
        private readonly Mock<ITodoRepository> _mockRepository;

        public CommandTests()
        {
            _mockRepository = new Mock<ITodoRepository>();
        }

        [Theory]
        [InlineData("Test Todo", "Test Description")]
        [InlineData("Another Todo", "Another Description")]
        [InlineData("Simple Todo", "")]
        public async Task CreateTodoCommandHandler_WithValidCommand_ShouldCreateTodoItem(string title, string description)
        {
            // Arrange
            var handler = new CreateTodoCommandHandler(_mockRepository.Object);
            var command = new CreateTodoCommand(title, description);
            var expectedTodo = new TodoItem(title, description);

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<TodoItem>()))
                .ReturnsAsync(expectedTodo);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.TodoItem);
            Assert.Equal(expectedTodo.Id, result.TodoItem.Id);
            Assert.Equal(expectedTodo.Title, result.TodoItem.Title);
            Assert.Equal(expectedTodo.Description, result.TodoItem.Description);

            _mockRepository.Verify(r => r.AddAsync(It.IsAny<TodoItem>()), Times.Once);
        }

        [Theory]
        [InlineData("", "Test Description")]
        [InlineData("   ", "Test Description")]
        [InlineData(null, "Test Description")]
        public async Task CreateTodoCommandHandler_WithInvalidTitle_ShouldThrowArgumentException(string invalidTitle, string description)
        {
            // Arrange
            var handler = new CreateTodoCommandHandler(_mockRepository.Object);
            var command = new CreateTodoCommand(invalidTitle, description);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                handler.Handle(command, CancellationToken.None));

            _mockRepository.Verify(r => r.AddAsync(It.IsAny<TodoItem>()), Times.Never);
        }

        [Theory]
        [InlineData("Test Todo")]
        [InlineData("Another Todo")]
        public async Task DeleteTodoCommandHandler_WhenTodoExists_ShouldDeleteTodo(string title)
        {
            // Arrange
            var handler = new DeleteTodoCommandHandler(_mockRepository.Object);
            var todo = new TodoItem(title);
            var command = new DeleteTodoCommand(todo.Id);

            _mockRepository.Setup(r => r.ExistsAsync(todo.Id))
                .ReturnsAsync(true);
            _mockRepository.Setup(r => r.DeleteAsync(todo.Id))
                .ReturnsAsync(true);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Contains("deleted", result.Message.ToLower());

            _mockRepository.Verify(r => r.ExistsAsync(todo.Id), Times.Once);
            _mockRepository.Verify(r => r.DeleteAsync(todo.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteTodoCommandHandler_WhenTodoDoesNotExist_ShouldReturnFailure()
        {
            // Arrange
            var handler = new DeleteTodoCommandHandler(_mockRepository.Object);
            var nonExistentId = Guid.NewGuid();
            var command = new DeleteTodoCommand(nonExistentId);

            _mockRepository.Setup(r => r.ExistsAsync(nonExistentId))
                .ReturnsAsync(false);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Contains("not found", result.Message.ToLower());

            _mockRepository.Verify(r => r.ExistsAsync(nonExistentId), Times.Once);
            _mockRepository.Verify(r => r.DeleteAsync(nonExistentId), Times.Never);
        }

        [Theory]
        [InlineData("Todo 1")]
        [InlineData("Todo 2")]
        [InlineData("Todo 3")]
        public async Task DeleteTodoCommandHandler_WithVariousIds_ShouldHandleCorrectly(string title)
        {
            // Arrange
            var handler = new DeleteTodoCommandHandler(_mockRepository.Object);
            var todo = new TodoItem(title);
            var command = new DeleteTodoCommand(todo.Id);

            _mockRepository.Setup(r => r.ExistsAsync(todo.Id))
                .ReturnsAsync(true);
            _mockRepository.Setup(r => r.DeleteAsync(todo.Id))
                .ReturnsAsync(true);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Contains("deleted", result.Message.ToLower());

            _mockRepository.Verify(r => r.ExistsAsync(todo.Id), Times.Once);
            _mockRepository.Verify(r => r.DeleteAsync(todo.Id), Times.Once);
        }
    }
} 