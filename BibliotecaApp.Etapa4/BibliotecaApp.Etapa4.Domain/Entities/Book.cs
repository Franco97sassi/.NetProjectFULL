namespace BibliotecaApp.Etapa4.Domain.Entities;

public class Book
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public int Stock { get; private set; }

    public void ActualizarStock(int nuevoStock)
    {
        if (nuevoStock < 0)
        {
            throw new ArgumentException("El stock no puede ser negativo.");
        }

        Stock = nuevoStock;
    }
}
