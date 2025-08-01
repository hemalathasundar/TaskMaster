using TaskMaster.Core.Entities;
using TaskMaster.Core.Interfaces;

namespace TaskMaster.Infrastructure.Repositories
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private readonly List<TodoItem> _todoItems = new List<TodoItem>();

        public Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return Task.FromResult(_todoItems.AsEnumerable());
        }

        public Task<TodoItem> GetByIdAsync(Guid id)
        {
            var todoItem = _todoItems.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(todoItem);
        }

        public Task<TodoItem> AddAsync(TodoItem todoItem)
        {
            _todoItems.Add(todoItem);
            return Task.FromResult(todoItem);
        }
        public Task<bool> DeleteAsync(Guid id)
        {
            var todoItem = _todoItems.FirstOrDefault(t => t.Id == id);
            if (todoItem != null)
            {
                _todoItems.Remove(todoItem);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            var exists = _todoItems.Any(t => t.Id == id);
            return Task.FromResult(exists);
        }
    }
}