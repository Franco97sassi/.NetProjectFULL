namespace BibliotecaApp.Exceptions;

public class StockInsuficienteException : Exception
{
    public StockInsuficienteException(string mensaje) : base(mensaje)
    {
    }
}