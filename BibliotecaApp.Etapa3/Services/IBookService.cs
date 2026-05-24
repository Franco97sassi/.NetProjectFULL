using BibliotecaApp.Etapa3.Models;

namespace BibliotecaApp.Etapa3.Services;

public interface IBookService
{
    Task<IReadOnlyList<Book>> GetAllAsync(string? author, int page, int pageSize);
    Task<Book?> GetByIdAsync(int id);
    Task<Book> CreateAsync(CreateBookRequest request);
    Task<Book?> UpdateStockAsync(int id, int stock);
    Task<bool> DeleteAsync(int id);
}
