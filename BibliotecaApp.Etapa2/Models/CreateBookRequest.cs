using System.ComponentModel.DataAnnotations;

namespace BibliotecaApp.Etapa2.Models;

public class CreateBookRequest
{
    [Required]
    [MinLength(2)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Author { get; set; } = string.Empty;

    [Range(0, 1000)]
    public int Stock { get; set; }
}
