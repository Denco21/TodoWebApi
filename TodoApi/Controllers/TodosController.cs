using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoLibrary.DataAccess;
using TodoLibrary.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ITodoData _data;
    private readonly ILogger<TodosController> _logger;

    public TodosController(ITodoData data, ILogger<TodosController> logger)
    {
        _data = data;
        _logger = logger;
    }

    private int GetUserId()     
    {
        var userIdText = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdText);
    }

    // GET: api/Todos
    [HttpGet(Name ="GetAllTodo")]
    public async Task<ActionResult<List<TodoModel>>> Get()
    {
        _logger.LogInformation("GET: api/Todos");

        try
        {
            var output = await _data.GetAllAssigned(GetUserId());

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The GET call to api/Todos failed.");
            return BadRequest();
        }
    }

    // GET api/Todos/5
    [HttpGet("{todoId}",Name ="GetOneTodo")]
    public async Task<ActionResult<TodoModel>> Get(int todoId)
    {
        _logger.LogInformation("GET: api/Todos/{TodoId}", todoId);

        try
        {
            var output = await _data.GetOneAssigned(GetUserId(), todoId);

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "The GET call to {ApiPath} failed. The Id was {TodoId}",
                $"api/Todos/Id",
                todoId);
            return BadRequest();
        }
    }

    // POST api/Todos
    [HttpPost(Name ="CreateTodo")]
    public async Task<ActionResult<TodoModel>> Post([FromBody] string task)
    {
        _logger.LogInformation("POST: api/Todos (Task: {Task})", task);

        try
        {
            var output = await _data.Create(GetUserId(), task);

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The POST call to api/Todos failed. Task value was {Task}", task);
            return BadRequest();
        }
    }

    // PUT api/Todos/5
    [HttpPut("{todoId}", Name ="UpdateTodoTask")]
    public async Task<ActionResult> Put(int todoId, [FromBody] string task)
    {
        _logger.LogInformation("PUT: api/Todos/{TodoId} (Task: {Task})", todoId, task);

        try
        {
            await _data.UpdateTask(GetUserId(), todoId, task);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "The PUT call to api/Todos/{TodoId} failed. Task value was {Task}",
                todoId,
                task);
            return BadRequest();
        }
    }

    // PUT api/Todos/5/Complete
    [HttpPut("{todoId}/Complete", Name ="CompleteTodo")]
    public async Task<IActionResult> Complete(int todoId)
    {
        _logger.LogInformation("PUT: api/Todos/{TodoId}/Complete", todoId);

        try
        {
            await _data.CompleteTodo(GetUserId(), todoId);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "The PUT call to api/Todos/{TodoId}/Complete failed.",
                todoId);
            return BadRequest();
        }
    }

    // DELETE api/Todos/5
    [HttpDelete("{todoId}", Name ="DeleteTodo")]
    public async Task<IActionResult> Delete(int todoId)
    {
        _logger.LogInformation("DELETE: api/Todos/{TodoId}", todoId);

        try
        {
            await _data.Delete(GetUserId(), todoId);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "The DELETE call to api/Todos/{TodoId} failed.",
                todoId);
            return BadRequest();
        }
    }
}

