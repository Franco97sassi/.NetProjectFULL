using BibliotecaApp.Etapa3.Models.Reports;

namespace BibliotecaApp.Etapa3.Services.Reports;

public interface IReportService
{
    Task<IReadOnlyList<AuthorStockReport>> GetAuthorStockReportAsync();
}
