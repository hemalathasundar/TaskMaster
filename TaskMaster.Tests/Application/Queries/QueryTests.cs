using Moq;
using TaskMaster.Application.Queries;
using TaskMaster.Core.Entities;
using TaskMaster.Core.Interfaces;

namespace TaskMaster.Tests.Application.Queries
{
    public class QueryTests
    {
        private readonly Mock<ITodoRepository> _mockRepository;

        public QueryTests()
        {
            _mockRepository = new Mock<ITodoRepository>();
        }

        [Fact]
        public async Task GetAllTodosQueryHandler_WhenNoTodos_ShouldReturnEmptyList()
        {
            // Arrange
            var handler = new GetAllTodosQueryHandler(_mockRepository.Object);
            var query = new GetAllTodosQuery();

            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<TodoItem>());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Todos);
            Assert.Empty(result.Todos);

            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetAllTodosQueryHandler_WhenTodosExist_ShouldReturnAllTodos(int todoCount)
        {
            // Arrange
            var handler = new GetAllTodosQueryHandler(_mockRepository.Object);
            var query = new GetAllTodosQuery();

            var todos = new List<TodoItem>();
            for (int i = 0; i < todoCount; i++)
            {
                todos.Add(new TodoItem($"Todo {i + 1}", $"Description {i + 1}"));
            }

            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(todos);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Todos);
            Assert.Equal(todoCount, result.Todos.Count());

            for (int i = 0; i < todoCount; i++)
            {
                Assert.Contains(result.Todos, t => t.Title == $"Todo {i + 1}");
            }

            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Theory]
        [InlineData("Todo 1", "Description 1")]
        [InlineData("Todo 2", "Description 2")]
        [InlineData("Todo 3", "")]
        public async Task GetAllTodosQueryHandler_WithVariousTodos_ShouldReturnCorrectData(string title, string description)
        {
            // Arrange
            var handler = new GetAllTodosQueryHandler(_mockRepository.Object);
            var query = new GetAllTodosQuery();

            var todo = new TodoItem(title, description);
            var todos = new List<TodoItem> { todo };

            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(todos);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Todos);
            Assert.Single(result.Todos);

            var returnedTodo = result.Todos.First();
            Assert.Equal(title, returnedTodo.Title);
            Assert.Equal(description, returnedTodo.Description);

            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
} 