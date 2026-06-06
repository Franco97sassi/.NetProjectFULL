namespace Users.Api.Models;
public sealed class UserItem { public Guid Id { get; set; } = Guid.NewGuid(); public string FullName { get; set; } = string.Empty; public bool Suspended { get; set; } }
