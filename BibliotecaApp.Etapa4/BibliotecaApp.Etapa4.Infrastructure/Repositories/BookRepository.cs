using BibliotecaApp.Etapa4.Domain.Entities;
using BibliotecaApp.Etapa4.Domain.Repositories;
using BibliotecaApp.Etapa4.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApp.Etapa4.Infrastructure.Repositories;

public class BookRepository(BibliotecaDbContext context) : IBookRepository
{
    public async Task<IReadOnlyCollection<Book>> GetAllAsync(CancellationToken ct = default)
        => await context.Books.OrderBy(x => x.Titulo).ToListAsync(ct);

    public Task<Book?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => context.Books.FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task AddAsync(Book book, CancellationToken ct = default)
        => await context.Books.AddAsync(book, ct);

  
}
