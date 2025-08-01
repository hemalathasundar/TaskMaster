using MediatR;
using TaskMaster.Application.DTOs;
using TaskMaster.Core.Entities;
using TaskMaster.Core.Interfaces;

namespace TaskMaster.Application.Commands
{
    public class CreateTodoCommand : IRequest<CreateTodoResponse>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public CreateTodoCommand(string title, string description = "")
        {
            Title = title;
            Description = description;
        }
    }

    public class CreateTodoResponse
    {
        public TodoItemResponse TodoItem { get; set; } = new();
    }

    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, CreateTodoResponse>
    {
        private readonly ITodoRepository _todoRepository;

        public CreateTodoCommandHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<CreateTodoResponse> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ArgumentException("Title cannot be null or empty", nameof(request.Title));

            var todoItem = new TodoItem(request.Title, request.Description);
            var createdTodo = await _todoRepository.AddAsync(todoItem);

            return new CreateTodoResponse
            {
                TodoItem = new TodoItemResponse
                {
                    Id = createdTodo.Id,
                    Title = createdTodo.Title,
                    Description = createdTodo.Description,
                    CreatedAt = createdTodo.CreatedAt,
                    UpdatedAt = createdTodo.UpdatedAt
                }
            };
        }
    }
}