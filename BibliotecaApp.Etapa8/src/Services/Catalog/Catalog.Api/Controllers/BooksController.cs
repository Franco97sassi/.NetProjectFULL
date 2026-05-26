using BibliotecaApp.Etapa8.Contracts.Events;
using Catalog.Api.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("api/catalog/books")]
public sealed class BooksController(List<BookItem> books, IPublishEndpoint publishEndpoint) : ControllerBase
{
    [HttpGet] public IActionResult Get() => Ok(books);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookItem model)
    {
        books.Add(model);
        await publishEndpoint.Publish(new BookRegistered(model.Id, model.Isbn, model.Title, model.Stock));
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }
}
