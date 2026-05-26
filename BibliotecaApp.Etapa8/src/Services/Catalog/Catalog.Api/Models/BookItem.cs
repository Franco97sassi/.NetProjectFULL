namespace Catalog.Api.Models;
public sealed class BookItem { public Guid Id { get; set; } = Guid.NewGuid(); public string Isbn { get; set; } = string.Empty; public string Title { get; set; } = string.Empty; public int Stock { get; set; } }
