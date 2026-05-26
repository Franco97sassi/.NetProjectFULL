using BibliotecaApp.Etapa8.Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Models;

namespace Users.Api.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController(List<UserItem> users, IPublishEndpoint publish) : ControllerBase
{
    [HttpGet] public IActionResult Get() => Ok(users);
    [HttpPost] public IActionResult Create([FromBody] UserItem model) { users.Add(model); return Ok(model); }

    [HttpPost("{id:guid}/suspend")]
    public async Task<IActionResult> Suspend(Guid id, [FromQuery] string reason = "manual")
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user is null) return NotFound();
        user.Suspended = true;
        await publish.Publish(new UserSuspended(user.Id, reason));
        return Ok(user);
    }
}
