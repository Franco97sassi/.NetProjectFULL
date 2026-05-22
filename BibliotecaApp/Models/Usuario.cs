namespace BibliotecaApp.Models;

public abstract class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public virtual void MostrarDatos()
    {
        Console.WriteLine($"Usuario: {Nombre} - {Email}");
    }
}