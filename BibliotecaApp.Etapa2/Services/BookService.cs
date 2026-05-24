using BibliotecaApp.Etapa2.Models;

namespace BibliotecaApp.Etapa2.Services;

public class BookService : IBookService
{
    private readonly List<Book> _books =
    [
        new() { Id = 1, Title = "Clean Code", Author = "Robert C. Martin", Stock = 3 },
        new() { Id = 2, Title = "Domain-Driven Design", Author = "Eric Evans", Stock = 2 }
    ];

    public IReadOnlyList<Book> GetAll() => _books;

    public IReadOnlyList<Book> GetByAuthor(string author) =>
        _books.Where(x => x.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();

    public Book? GetById(int id) => _books.FirstOrDefault(x => x.Id == id);

    public Book Create(CreateBookRequest request)
    {
        var nextId = _books.Max(x => x.Id) + 1;

        var book = new Book
        {
            Id = nextId,
            Title = request.Title,
            Author = request.Author,
            Stock = request.Stock
        };

        _books.Add(book);
        return book;
    }
}
