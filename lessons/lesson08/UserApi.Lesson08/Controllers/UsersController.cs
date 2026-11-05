using Microsoft.AspNetCore.Mvc;
using UserApi.Lesson08.Dtos;
using UserApi.Lesson08.Entities;
using UserApi.Lesson08.Repositories;

namespace UserApi.Lesson08.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(UserRepository repository) : ControllerBase
{
    private const string ValidClientId = "Client123";
    private const string ValidApiKey = "Secret123";

    [HttpPost("create")]
    public ActionResult<User> Create(
        [FromHeader(Name = "Client-Id")] string? clientId,
        CreateUserRequest request)
    {
        if (clientId != ValidClientId)
        {
            return Unauthorized(new { error = "Missing or invalid Client-Id header." });
        }

        var user = repository.Add(request);
        if (user is null)
        {
            return Conflict(new { error = "Email is already used by another user." });
        }

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpGet("{id:int}")]
    public ActionResult<User> GetById(
        int id,
        [FromHeader(Name = "ApiKey")] string? apiKey)
    {
        if (apiKey != ValidApiKey)
        {
            return Unauthorized(new { error = "Missing or invalid ApiKey header." });
        }

        var user = repository.GetById(id);
        if (user is null)
        {
            return NotFound(new { error = $"User {id} was not found." });
        }

        return user;
    }

    [HttpGet("search")]
    public ActionResult<List<User>> Search([FromQuery] UserFilter filter)
    {
        return repository.Search(filter);
    }
}
