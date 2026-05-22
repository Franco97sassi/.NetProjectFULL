namespace BibliotecaApp.Models;

public class Libro
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal Precio { get; set; }
    public bool Activo { get; set; } = true;
}