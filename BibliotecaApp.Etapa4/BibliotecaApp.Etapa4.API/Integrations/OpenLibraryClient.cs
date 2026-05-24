namespace BibliotecaApp.Etapa4.API.Integrations;

public interface IOpenLibraryClient
{
    Task<object> SearchByIsbnAsync(string isbn, CancellationToken ct);
}

public class OpenLibraryClient(HttpClient httpClient) : IOpenLibraryClient
{
    public async Task<object> SearchByIsbnAsync(string isbn, CancellationToken ct)
    {
        var response = await httpClient.GetAsync($"/isbn/{isbn}.json", ct);
        if (!response.IsSuccessStatusCode)
        {
            return new { isbn, found = false, statusCode = (int)response.StatusCode };
        }

        var payload = await response.Content.ReadAsStringAsync(ct);
        return new { isbn, found = true, source = "OpenLibrary", data = payload };
    }
}
