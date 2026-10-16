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

        return Created($"/api/users/{user.Id}", user);
    }
}
