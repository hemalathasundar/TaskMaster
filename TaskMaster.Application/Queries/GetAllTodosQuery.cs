using MediatR;
using TaskMaster.Application.DTOs;
using TaskMaster.Core.Interfaces;

namespace TaskMaster.Application.Queries
{
    public class GetAllTodosQuery : IRequest<GetAllTodosResponse>
    {
    }

    public class GetAllTodosResponse
    {
        public List<TodoItemResponse> Todos { get; set; } = new List<TodoItemResponse>();
    }

    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, GetAllTodosResponse>
    {
        private readonly ITodoRepository _todoRepository;

        public GetAllTodosQueryHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<GetAllTodosResponse> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
        {
            var todos = await _todoRepository.GetAllAsync();
            var response = new GetAllTodosResponse();

            foreach (var todo in todos)
            {
                response.Todos.Add(new TodoItemResponse
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    CreatedAt = todo.CreatedAt,
                    UpdatedAt = todo.UpdatedAt
                });
            }

            return response;
        }
    }
}