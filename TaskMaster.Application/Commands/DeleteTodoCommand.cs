using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TaskMaster.Core.Interfaces;

namespace TaskMaster.Application.Commands
{
    public class DeleteTodoCommand : IRequest<DeleteTodoResponse>
    {
        public Guid Id { get; set; }

        public DeleteTodoCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteTodoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, DeleteTodoResponse>
    {
        private readonly ITodoRepository _todoRepository;

        public DeleteTodoCommandHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<DeleteTodoResponse> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var exists = await _todoRepository.ExistsAsync(request.Id);
            if (!exists)
            {
                return new DeleteTodoResponse
                {
                    Success = false,
                    Message = "Todo item not found"
                };
            }

            var deleted = await _todoRepository.DeleteAsync(request.Id);
            
            return new DeleteTodoResponse
            {
                Success = deleted,
                Message = deleted ? "Todo item deleted successfully" : "Failed to delete todo item"
            };
        }
    }
} 