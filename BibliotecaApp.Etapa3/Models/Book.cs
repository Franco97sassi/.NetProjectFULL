using System.ComponentModel.DataAnnotations;

namespace BibliotecaApp.Etapa3.Models;

public class Book
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(80)]
    public string Author { get; set; } = string.Empty;

    [Range(0, 999)]
    public int Stock { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
