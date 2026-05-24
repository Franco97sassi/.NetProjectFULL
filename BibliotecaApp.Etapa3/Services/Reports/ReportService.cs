using BibliotecaApp.Etapa3.Models.Reports;
using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace BibliotecaApp.Etapa3.Services.Reports;

public class ReportService(IConfiguration configuration) : IReportService
{
    public async Task<IReadOnlyList<AuthorStockReport>> GetAuthorStockReportAsync()
    {
        var provider = configuration["Database:Provider"]?.ToLowerInvariant() ?? "inmemory";
        var connectionString = provider switch
        {
            "sqlserver" => configuration.GetConnectionString("SqlServer"),
            "postgres" => configuration.GetConnectionString("Postgres"),
            _ => null
        };

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return [];
        }

        if (provider == "sqlserver")
        {
            await using var sqlConnection = new SqlConnection(connectionString);
            var result = await sqlConnection.QueryAsync<AuthorStockReport>(
                "EXEC dbo.sp_GetAuthorStockReport");
            return result.ToList();
        }

        if (provider == "postgres")
        {
            await using var npgsqlConnection = new NpgsqlConnection(connectionString);
            var result = await npgsqlConnection.QueryAsync<AuthorStockReport>(
                "SELECT * FROM public.sp_get_author_stock_report()");
            return result.ToList();
        }

        return [];
    }
}
