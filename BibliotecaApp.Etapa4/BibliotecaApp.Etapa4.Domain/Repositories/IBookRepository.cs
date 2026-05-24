using BibliotecaApp.Etapa4.Domain.Entities;

namespace BibliotecaApp.Etapa4.Domain.Repositories;

public interface IBookRepository
{
    Task<IReadOnlyCollection<Book>> GetAllAsync(CancellationToken ct = default);
    Task<Book?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Book book, CancellationToken ct = default);
 }
