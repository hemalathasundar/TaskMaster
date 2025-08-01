namespace TaskMaster.Application.DTOs
{
    /// <summary>
    /// Response DTO for TodoItem data transfer
    /// </summary>
    public class TodoItemResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}