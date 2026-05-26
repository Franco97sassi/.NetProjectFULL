namespace Loans.Api.Models;
public sealed class LoanItem { public Guid Id { get; set; } = Guid.NewGuid(); public Guid BookId { get; set; } public Guid UserId { get; set; } public DateTime DueDateUtc { get; set; } = DateTime.UtcNow.AddDays(14); public bool Returned { get; set; } }
