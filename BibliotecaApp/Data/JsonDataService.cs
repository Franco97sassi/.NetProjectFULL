using System.Text.Json;

namespace BibliotecaApp.Data;

public class JsonDataService
{
    public async Task GuardarAsync<T>(string ruta, List<T> datos)
    {
        var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        await File.WriteAllTextAsync(ruta, json);
    }

    public async Task<List<T>> CargarAsync<T>(string ruta)
    {
        if (!File.Exists(ruta))
        {
            return new List<T>();
        }

        var json = await File.ReadAllTextAsync(ruta);

        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }
}