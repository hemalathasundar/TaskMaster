using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Application.Commands;
using TaskMaster.Application.DTOs;
using TaskMaster.Application.Queries;

namespace TaskMaster.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all todo items
        /// </summary>
        /// <returns>List of all todo items</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllTodosQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Create a new todo item
        /// </summary>
        /// <param name="request">The todo item creation request</param>
        /// <returns>The created todo item</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoItemRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateTodoCommand(request.Title, request.Description);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetAll), result.TodoItem);
        }

        /// <summary>
        /// Delete a todo item
        /// </summary>
        /// <param name="id">The ID of the todo item to delete</param>
        /// <returns>Success status</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteTodoCommand(id);
            var result = await _mediator.Send(command);

            if (!result.Success)
                return NotFound(new { message = result.Message });

            return Ok(new { message = result.Message });
        }
    }
}