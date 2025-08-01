using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Application.DTOs
{
    /// <summary>
    /// Request DTO for creating TodoItem
    /// </summary>
    public class TodoItemRequest
    {
        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; } = string.Empty;
    }
}