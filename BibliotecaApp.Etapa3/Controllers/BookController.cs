using BibliotecaApp.Etapa3.Models;
using BibliotecaApp.Etapa3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApp.Etapa3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Book>>> GetAll([FromQuery] string? author, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var books = await bookService.GetAllAsync(author, Math.Max(page, 1), Math.Clamp(pageSize, 1, 50));
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetById([FromRoute] int id)
    {
        var book = await bookService.GetByIdAsync(id);
        return book is null ? NotFound() : Ok(book);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Book>> Create([FromBody] CreateBookRequest request)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var created = await bookService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id:int}/stock")]
    public async Task<ActionResult<Book>> UpdateStock([FromRoute] int id, [FromBody] UpdateBookStockRequest request)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var updated = await bookService.UpdateStockAsync(id, request.Stock);
        return updated is null ? NotFound() : Ok(updated);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deleted = await bookService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}

