namespace BibliotecaApp.Etapa3.Models.Reports;

public class AuthorStockReport
{
    public string Author { get; set; } = string.Empty;
    public int BooksCount { get; set; }
    public int TotalStock { get; set; }
}
