using BibliotecaApp.Etapa3.Models.Reports;
using BibliotecaApp.Etapa3.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApp.Etapa3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController(IReportService reportService) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet("author-stock")]
    public async Task<ActionResult<IReadOnlyList<AuthorStockReport>>> GetAuthorStock()
    {
        var report = await reportService.GetAuthorStockReportAsync();
        return Ok(report);
    }
}
