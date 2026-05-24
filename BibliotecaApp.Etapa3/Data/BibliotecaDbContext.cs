using BibliotecaApp.Etapa3.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApp.Etapa3.Data;

public class BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
}
