using TaskMaster.Core.Entities;

namespace TaskMaster.Core.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem> GetByIdAsync(Guid id);
        Task<TodoItem> AddAsync(TodoItem todoItem);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}