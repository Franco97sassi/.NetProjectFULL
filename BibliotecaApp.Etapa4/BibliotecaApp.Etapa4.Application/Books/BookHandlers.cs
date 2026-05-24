using BibliotecaApp.Etapa4.Application.Common;
using BibliotecaApp.Etapa4.Domain.Entities;
using BibliotecaApp.Etapa4.Domain.Repositories;
using MediatR;

namespace BibliotecaApp.Etapa4.Application.Books;

public class CreateBookCommandHandler(IBookRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateBookCommand, Result<BookDto>>
{
    public async Task<Result<BookDto>> Handle(CreateBookCommand cmd, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(cmd.Titulo) || string.IsNullOrWhiteSpace(cmd.Autor))
        {
            return Result<BookDto>.Fail("Título y autor son requeridos.");
        }

        if (cmd.StockInicial < 0)
        {
            return Result<BookDto>.Fail("Stock inicial inválido.");
        }

        var book = new Book
        {
            Id = Guid.NewGuid(),
            Titulo = cmd.Titulo.Trim(),
            Autor = cmd.Autor.Trim()
        };

        book.ActualizarStock(cmd.StockInicial);

        await repository.AddAsync(book, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return Result<BookDto>.Ok(new BookDto(book.Id, book.Titulo, book.Autor, book.Stock));
    }
}

public class UpdateStockCommandHandler(IBookRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateStockCommand, Result<BookDto>>
{
    public async Task<Result<BookDto>> Handle(UpdateStockCommand cmd, CancellationToken ct)
    {
        var book = await repository.GetByIdAsync(cmd.BookId, ct);
        if (book is null)
        {
            return Result<BookDto>.Fail("Libro no encontrado.");
        }

        try
        {
            book.ActualizarStock(cmd.NuevoStock);
        }
        catch (ArgumentException ex)
        {
            return Result<BookDto>.Fail(ex.Message);
        }

        await unitOfWork.SaveChangesAsync(ct);

        return Result<BookDto>.Ok(new BookDto(book.Id, book.Titulo, book.Autor, book.Stock));
    }
}

public class GetAllBooksQueryHandler(IBookRepository repository)
    : IRequestHandler<GetAllBooksQuery, IReadOnlyCollection<BookDto>>
{
    public async Task<IReadOnlyCollection<BookDto>> Handle(GetAllBooksQuery request, CancellationToken ct)
    {
        var books = await repository.GetAllAsync(ct);
        return books.Select(x => new BookDto(x.Id, x.Titulo, x.Autor, x.Stock)).ToArray();
    }
}