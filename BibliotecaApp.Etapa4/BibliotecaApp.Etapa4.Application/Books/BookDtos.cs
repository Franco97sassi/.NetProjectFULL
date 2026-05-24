using BibliotecaApp.Etapa4.Application.Common;
using MediatR;

namespace BibliotecaApp.Etapa4.Application.Books;

public sealed record BookDto(Guid Id, string Titulo, string Autor, int Stock);

public sealed record CreateBookCommand(string Titulo, string Autor, int StockInicial)
    : IRequest<Result<BookDto>>;

public sealed record UpdateStockCommand(Guid BookId, int NuevoStock)
    : IRequest<Result<BookDto>>;

public sealed record GetAllBooksQuery()
    : IRequest<IReadOnlyCollection<BookDto>>;