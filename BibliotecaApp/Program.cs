using BibliotecaApp.Data;
using BibliotecaApp.Exceptions;
using BibliotecaApp.Models;
using BibliotecaApp.Services;

LibroService libroService = new();
PrestamoService prestamoService = new();
JsonDataService jsonDataService = new();

Cliente cliente = new()
{
    Id = 1,
    Nombre = "Franco",
    Email = "franco@email.com",
    Puntos = 10
};

Empleado empleado = new()
{
    Id = 2,
    Nombre = "Carlos",
    Email = "carlos@email.com",
    Cargo = "Administrador"
};

Libro libro1 = new()
{
    Id = 1,
    Titulo = "Clean Code",
    Autor = "Robert C. Martin",
    Stock = 3,
    Precio = 25000m
};

Libro libro2 = new()
{
    Id = 2,
    Titulo = "C# Avanzado",
    Autor = "Microsoft",
    Stock = 0,
    Precio = 18000m
};

libroService.AgregarLibro(libro1);
libroService.AgregarLibro(libro2);

Console.WriteLine("=== USUARIOS ===");

cliente.MostrarDatos();
empleado.MostrarDatos();

Console.WriteLine();

Console.WriteLine("=== LIBROS ===");

foreach (var libro in libroService.ObtenerTodos())
{
    Console.WriteLine($"{libro.Id} - {libro.Titulo} - Stock: {libro.Stock}");
}

Console.WriteLine();

Console.WriteLine("=== BÚSQUEDA LINQ ===");

var resultados = libroService.BuscarPorTitulo("clean");

foreach (var libro in resultados)
{
    Console.WriteLine($"Encontrado: {libro.Titulo}");
}

Console.WriteLine();

Console.WriteLine("=== PRÉSTAMO ===");

try
{
    prestamoService.RealizarPrestamo(libro1, cliente);
    Console.WriteLine("Préstamo realizado correctamente.");
}
catch (StockInsuficienteException ex)
{
    Console.WriteLine($"Error de stock: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error general: {ex.Message}");
}
finally
{
    Console.WriteLine("Proceso de préstamo finalizado.");
}

Console.WriteLine($"Stock actual de {libro1.Titulo}: {libro1.Stock}");

Console.WriteLine();

Console.WriteLine("=== TYPE CASTING / PARSING ===");

Console.Write("Ingrese un ID de libro: ");
string? entrada = Console.ReadLine();

if (int.TryParse(entrada, out int idLibro))
{
    var libroBuscado = libroService.BuscarPorId(idLibro);

    if (libroBuscado != null)
    {
        Console.WriteLine($"Libro encontrado: {libroBuscado.Titulo}");
    }
    else
    {
        Console.WriteLine("Libro no encontrado.");
    }
}
else
{
    Console.WriteLine("El valor ingresado no es un número válido.");
}

Console.WriteLine();

Console.WriteLine("=== DICCIONARIO ===");

Dictionary<int, Libro> diccionarioLibros = libroService.ObtenerDiccionario();

foreach (var item in diccionarioLibros)
{
    Console.WriteLine($"Clave: {item.Key} - Libro: {item.Value.Titulo}");
}

Console.WriteLine();

Console.WriteLine("=== GUARDAR JSON ASYNC ===");

await jsonDataService.GuardarAsync("libros.json", libroService.ObtenerTodos());

Console.WriteLine("Libros guardados correctamente.");