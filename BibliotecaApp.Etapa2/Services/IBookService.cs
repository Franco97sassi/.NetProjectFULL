using BibliotecaApp.Etapa2.Models;

namespace BibliotecaApp.Etapa2.Services;

public interface IBookService
{
    IReadOnlyList<Book> GetAll();
    IReadOnlyList<Book> GetByAuthor(string author);
    Book? GetById(int id);
    Book Create(CreateBookRequest request);
}
