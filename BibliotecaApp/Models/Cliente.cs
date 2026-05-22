namespace BibliotecaApp.Models;

public class Cliente : Usuario
{
    public int Puntos { get; set; }

    public override void MostrarDatos()
    {
        Console.WriteLine($"Cliente: {Nombre} - Puntos: {Puntos}");
    }
}