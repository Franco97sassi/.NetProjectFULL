using BibliotecaApp.Models;
using BibliotecaApp.Repositories;

namespace BibliotecaApp.Services;

public class LibroService
{
    private readonly Repository<Libro> _repository = new();

    public void AgregarLibro(Libro libro)
    {
        if (string.IsNullOrWhiteSpace(libro.Titulo))
        {
            throw new Exception("El título es obligatorio.");
        }

        _repository.Agregar(libro);
    }

    public List<Libro> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public Libro? BuscarPorId(int id)
    {
        return _repository.BuscarPorId(id);
    }

    public List<Libro> BuscarPorTitulo(string titulo)
    {
        return _repository.ObtenerTodos()
            .Where(x => x.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Libro> ObtenerSinStock()
    {
        return _repository.ObtenerTodos()
            .Where(x => x.Stock == 0)
            .ToList();
    }

    public List<Libro> OrdenarPorTitulo()
    {
        return _repository.ObtenerTodos()
            .OrderBy(x => x.Titulo)
            .ToList();
    }

    public bool ExisteLibro(int id)
    {
        return _repository.ObtenerTodos()
            .Any(x => x.Id == id);
    }

    public Dictionary<int, Libro> ObtenerDiccionario()
    {
        return _repository.ObtenerTodos()
            .ToDictionary(x => x.Id, x => x);
    }
}