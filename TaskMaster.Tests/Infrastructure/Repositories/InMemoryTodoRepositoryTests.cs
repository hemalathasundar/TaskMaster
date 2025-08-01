using TaskMaster.Core.Entities;
using TaskMaster.Infrastructure.Repositories;

namespace TaskMaster.Tests.Infrastructure.Repositories
{
    public class InMemoryTodoRepositoryTests
    {
        private readonly InMemoryTodoRepository _repository;

        public InMemoryTodoRepositoryTests()
        {
            _repository = new InMemoryTodoRepository();
        }

        [Fact]
        public async Task GetAllAsync_WhenNoItems_ShouldReturnEmptyList()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetAllAsync_WhenItemsExist_ShouldReturnAllItems(int itemCount)
        {
            // Arrange
            var todos = new List<TodoItem>();
            for (int i = 0; i < itemCount; i++)
            {
                var todo = new TodoItem($"Test {i + 1}");
                todos.Add(todo);
                await _repository.AddAsync(todo);
            }

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.Equal(itemCount, result.Count());
            foreach (var todo in todos)
            {
                Assert.Contains(result, t => t.Id == todo.Id);
            }
        }

        [Theory]
        [InlineData("Test Todo")]
        [InlineData("Another Todo")]
        [InlineData("Simple Todo")]
        public async Task AddAsync_ShouldAddItemToRepository(string title)
        {
            // Arrange
            var todo = new TodoItem(title);

            // Act
            var result = await _repository.AddAsync(todo);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(todo.Id, result.Id);

            var allItems = await _repository.GetAllAsync();
            Assert.Single(allItems);
            Assert.Contains(allItems, t => t.Id == todo.Id);
        }

        [Theory]
        [InlineData("Test Todo")]
        [InlineData("Another Todo")]
        public async Task DeleteAsync_WhenItemExists_ShouldRemoveItem(string title)
        {
            // Arrange
            var todo = new TodoItem(title);
            await _repository.AddAsync(todo);

            // Act
            var result = await _repository.DeleteAsync(todo.Id);

            // Assert
            Assert.True(result);

            var allItems = await _repository.GetAllAsync();
            Assert.Empty(allItems);

            var deletedItem = await _repository.GetByIdAsync(todo.Id);
            Assert.Null(deletedItem);
        }

        [Fact]
        public async Task DeleteAsync_WhenItemDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = await _repository.DeleteAsync(nonExistentId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("Todo 1", "Description 1")]
        [InlineData("Todo 2", "Description 2")]
        [InlineData("Todo 3", "")]
        public async Task AddAsync_WithVariousInputs_ShouldAddItemCorrectly(string title, string description)
        {
            // Arrange
            var todo = new TodoItem(title, description);

            // Act
            var result = await _repository.AddAsync(todo);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(todo.Id, result.Id);
            Assert.Equal(title, result.Title);
            Assert.Equal(description, result.Description);

            var allItems = await _repository.GetAllAsync();
            Assert.Single(allItems);
            Assert.Contains(allItems, t => t.Id == todo.Id);
        }
    }
}