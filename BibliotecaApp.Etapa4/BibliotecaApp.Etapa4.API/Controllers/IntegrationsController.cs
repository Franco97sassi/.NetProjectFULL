using BibliotecaApp.Etapa4.API.Integrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApp.Etapa4.API.Controllers;

[ApiController]
[Route("api/integrations")]
[Authorize]
public class IntegrationsController(IOpenLibraryClient openLibraryClient) : ControllerBase
{
    [HttpGet("openlibrary/isbn/{isbn}")]
    public async Task<IActionResult> SearchByIsbn(string isbn, CancellationToken ct)
        => Ok(await openLibraryClient.SearchByIsbnAsync(isbn, ct));
}
