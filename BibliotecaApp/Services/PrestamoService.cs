using BibliotecaApp.Exceptions;
using BibliotecaApp.Models;

namespace BibliotecaApp.Services;

public class PrestamoService
{
    private readonly List<Prestamo> _prestamos = new();

    public void RealizarPrestamo(Libro libro, Cliente cliente)
    {
        if (libro.Stock <= 0)
        {
            throw new StockInsuficienteException("No hay stock disponible para este libro.");
        }

        libro.Stock--;

        var prestamo = new Prestamo
        {
            Id = _prestamos.Count + 1,
            LibroId = libro.Id,
            ClienteId = cliente.Id,
            FechaPrestamo = DateTime.Now
        };

        _prestamos.Add(prestamo);
    }

    public void DevolverLibro(int prestamoId, Libro libro)
    {
        var prestamo = _prestamos.FirstOrDefault(x => x.Id == prestamoId);

        if (prestamo == null)
        {
            throw new Exception("Préstamo no encontrado.");
        }

        prestamo.FechaDevolucion = DateTime.Now;
        libro.Stock++;
    }

    public List<Prestamo> ObtenerPrestamosActivos()
    {
        return _prestamos
            .Where(x => x.FechaDevolucion == null)
            .ToList();
    }
}