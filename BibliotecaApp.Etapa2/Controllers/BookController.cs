using BibliotecaApp.Etapa2.Models;
using BibliotecaApp.Etapa2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApp.Etapa2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyList<Book>> GetAll([FromQuery] string? author)
    {
        var books = string.IsNullOrWhiteSpace(author)
            ? bookService.GetAll()
            : bookService.GetByAuthor(author);

        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Book> GetById([FromRoute] int id)
    {
        var book = bookService.GetById(id);
        return book is null ? NotFound() : Ok(book);
    }

    [Authorize]
    [HttpPost]
    public ActionResult<Book> Create([FromBody] CreateBookRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var created = bookService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}
