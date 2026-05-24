using BibliotecaApp.Etapa4.API.Hubs;
using BibliotecaApp.Etapa4.Application.Books;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
namespace BibliotecaApp.Etapa4.API.Controllers;

[ApiController]
[Route("api/books")]
[Authorize]
public class BooksController(IMediator mediator, Microsoft.AspNetCore.SignalR.IHubContext<LibraryHub> hubContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllBooksQuery(), ct));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateBookCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        if (!result.Success)
            return BadRequest(result.Error);

        await hubContext.Clients.All.SendAsync("BookCreated", result.Value, ct);
        return Ok(result.Value);
    }

    [HttpPatch("{id:guid}/stock")]
    public async Task<IActionResult> UpdateStock(Guid id, [FromBody] int nuevoStock, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateStockCommand(id, nuevoStock), ct);
        if (!result.Success)
            return NotFound(result.Error);

        await hubContext.Clients.All.SendAsync("BookStockUpdated", result.Value, ct);
        return Ok(result.Value);
    }
}
