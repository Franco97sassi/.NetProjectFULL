namespace BibliotecaApp.Models;

public class Empleado : Usuario
{
    public string Cargo { get; set; } = string.Empty;

    public override void MostrarDatos()
    {
        Console.WriteLine($"Empleado: {Nombre} - Cargo: {Cargo}");
    }
}