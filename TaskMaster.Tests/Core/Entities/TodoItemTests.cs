using TaskMaster.Core.Entities;

namespace TaskMaster.Tests.Core.Entities
{
    public class TodoItemTests
    {
        [Fact]
        public void Constructor_WithValidTitle_ShouldCreateTodoItem()
        {
            // Arrange
            var title = "Test Todo";
            var description = "Test Description";

            // Act
            var todoItem = new TodoItem(title, description);

            // Assert
            Assert.NotEqual(Guid.Empty, todoItem.Id);
            Assert.Equal(title, todoItem.Title);
            Assert.Equal(description, todoItem.Description);
            Assert.True(todoItem.CreatedAt > DateTime.UtcNow.AddMinutes(-1));
            Assert.Null(todoItem.UpdatedAt);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_WithInvalidTitle_ShouldThrowException(string invalidTitle)
        {
            // Arrange & Act & Assert
            if (invalidTitle == null)
            {
                Assert.Throws<ArgumentNullException>(() => new TodoItem(invalidTitle));
            }
            else
            {
                Assert.Throws<ArgumentException>(() => new TodoItem(invalidTitle));
            }
        }

        [Theory]
        [InlineData("Updated Title")]
        [InlineData("New Title")]
        [InlineData("Another Title")]
        public void UpdateTitle_WithValidTitle_ShouldUpdateTitle(string newTitle)
        {
            // Arrange
            var todoItem = new TodoItem("Original Title");

            // Act
            todoItem.UpdateTitle(newTitle);

            // Assert
            Assert.Equal(newTitle, todoItem.Title);
            Assert.NotNull(todoItem.UpdatedAt);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void UpdateTitle_WithInvalidTitle_ShouldThrowArgumentException(string invalidTitle)
        {
            // Arrange
            var todoItem = new TodoItem("Original Title");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => todoItem.UpdateTitle(invalidTitle));
        }

        [Theory]
        [InlineData("Updated Description")]
        [InlineData("New Description")]
        [InlineData("Another Description")]
        public void UpdateDescription_WithValidDescription_ShouldUpdateDescription(string newDescription)
        {
            // Arrange
            var todoItem = new TodoItem("Test Title", "Original Description");

            // Act
            todoItem.UpdateDescription(newDescription);

            // Assert
            Assert.Equal(newDescription, todoItem.Description);
            Assert.NotNull(todoItem.UpdatedAt);
        }

        [Theory]
        [InlineData("Task 1", "Description 1")]
        [InlineData("Task 2", "Description 2")]
        [InlineData("Task 3", "")]
        public void Constructor_WithVariousValidInputs_ShouldCreateValidTodoItem(string title, string description)
        {
            // Act
            var todoItem = new TodoItem(title, description);

            // Assert
            Assert.NotEqual(Guid.Empty, todoItem.Id);
            Assert.Equal(title, todoItem.Title);
            Assert.Equal(description, todoItem.Description);
            Assert.True(todoItem.CreatedAt > DateTime.UtcNow.AddMinutes(-1));
            Assert.Null(todoItem.UpdatedAt);
        }
    }
}