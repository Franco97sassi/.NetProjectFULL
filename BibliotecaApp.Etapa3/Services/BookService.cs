using BibliotecaApp.Etapa3.Data;
using BibliotecaApp.Etapa3.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApp.Etapa3.Services;

public class BookService(BibliotecaDbContext db) : IBookService
{
    public async Task<IReadOnlyList<Book>> GetAllAsync(string? author, int page, int pageSize)
    {
        var query = db.Books.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(author))
        {
            query = query.Where(b => b.Author.Contains(author));
        }

        return await query
            .OrderByDescending(b => b.CreatedAtUtc)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public Task<Book?> GetByIdAsync(int id) => db.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);

    public async Task<Book> CreateAsync(CreateBookRequest request)
    {
        var book = new Book { Title = request.Title, Author = request.Author, Stock = request.Stock };
        db.Books.Add(book);
        await db.SaveChangesAsync();
        return book;
    }

    public async Task<Book?> UpdateStockAsync(int id, int stock)
    {
        var book = await db.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book is null) return null;

        book.Stock = stock;
        await db.SaveChangesAsync();
        return book;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await db.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book is null) return false;

        db.Books.Remove(book);
        await db.SaveChangesAsync();
        return true;
    }
}
