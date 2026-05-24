using BibliotecaApp.Etapa4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BibliotecaApp.Etapa4.Infrastructure.Data;

public class BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
}
