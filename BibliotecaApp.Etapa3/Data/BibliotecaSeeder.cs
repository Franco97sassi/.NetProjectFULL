using BibliotecaApp.Etapa3.Models;

namespace BibliotecaApp.Etapa3.Data;

public static class BibliotecaSeeder
{
	public static void Seed(BibliotecaDbContext db)
	{
		if (db.Books.Any()) return;

		db.Books.AddRange(
			new Book { Title = "Clean Code", Author = "Robert C. Martin", Stock = 5 },
			new Book { Title = "Domain-Driven Design", Author = "Eric Evans", Stock = 3 },
			new Book { Title = "Refactoring", Author = "Martin Fowler", Stock = 4 }
		);
		db.SaveChanges();
	}
}
