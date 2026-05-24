namespace BibliotecaApp.Etapa4.Application.Books;

public sealed record CreateBookCommand(string Titulo, string Autor, int StockInicial);
public sealed record UpdateStockCommand(Guid BookId, int NuevoStock);
public sealed record BookDto(Guid Id, string Titulo, string Autor, int Stock);
