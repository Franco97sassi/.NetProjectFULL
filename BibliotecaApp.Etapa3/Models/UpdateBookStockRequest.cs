using System.ComponentModel.DataAnnotations;

namespace BibliotecaApp.Etapa3.Models;

public class UpdateBookStockRequest
{
    [Range(0, 999)]
    public int Stock { get; set; }
}
