using Microsoft.AspNetCore.Mvc;
using UserApi.Data;
using UserApi.Entities;
using UserApi.Models;

namespace UserApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(UserRepository repository) : ControllerBase
{
    private const string ValidClientId = "Client123";

    [HttpPost("create")]
    public ActionResult<User> Create(
        [FromHeader(Name = "Client-Id")] string? clientId,//sem se uložá ten klíč
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

        return Created($"/api/users/{user.Id}", user);
    }

    private const string ValidApiKey = "Secret123";

    [HttpGet("{id:int}")]//"adresa" konkrétního usera
    public ActionResult<User> GetById([FromHeader(Name = "ApiKey")] string? ApiKey, int Id)
    {
        if (ValidApiKey != ApiKey)
        {
            return Unauthorized(new { error = "Missing or invalid Api-Key header." });
        }

        var FoundUser = repository.GetById(Id);
        if (FoundUser == null)
        { 
            return NotFound(new { error = "User not found." });
        }
        else
        {
            return Ok(FoundUser);
        }
    }

    [HttpGet("search")]//tady to není adresa konkrétního uživatele, ale filtr

    public ActionResult<List<User>> Search(string? name, int? min_age, int? max_age, int? limit)
    {
        List<User> SearchedUsers = new List<User>();

        if (limit <= 0)
            return BadRequest(new { error = "Limit has invalid value, must be greater than 0." });

        SearchedUsers = repository.Search(name, min_age, max_age, limit);

        return Ok(SearchedUsers);
    }
}
