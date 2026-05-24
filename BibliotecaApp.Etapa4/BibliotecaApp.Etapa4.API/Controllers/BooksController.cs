using BibliotecaApp.Etapa4.Application.Books;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApp.Etapa4.API.Controllers;

[ApiController]
[Route("api/books")]
[Authorize]
public class BooksController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllBooksQuery(), ct));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateBookCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return result.Success ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPatch("{id:guid}/stock")]
    public async Task<IActionResult> UpdateStock(Guid id, [FromBody] int nuevoStock, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateStockCommand(id, nuevoStock), ct);
        return result.Success ? Ok(result.Value) : NotFound(result.Error);
    }
}
