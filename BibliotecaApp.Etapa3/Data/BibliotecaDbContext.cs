using BibliotecaApp.Etapa3.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApp.Etapa3.Data;

public class BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasIndex(b => b.Author);
            entity.HasIndex(b => b.CreatedAtUtc);
            entity.HasIndex(b => new { b.Author, b.CreatedAtUtc });
        });
    }
}
