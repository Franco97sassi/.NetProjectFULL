using BibliotecaApp.Etapa4.Application.Books;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApp.Etapa4.API.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController(BookAppService appService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct)
        => Ok(await appService.GetAllAsync(ct));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateBookCommand command, CancellationToken ct)
    {
        var result = await appService.CreateAsync(command, ct);
        return result.Success ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPatch("{id:guid}/stock")]
    public async Task<IActionResult> UpdateStock(Guid id, [FromBody] int nuevoStock, CancellationToken ct)
    {
        var result = await appService.UpdateStockAsync(new UpdateStockCommand(id, nuevoStock), ct);
        return result.Success ? Ok(result.Value) : NotFound(result.Error);
    }
}
