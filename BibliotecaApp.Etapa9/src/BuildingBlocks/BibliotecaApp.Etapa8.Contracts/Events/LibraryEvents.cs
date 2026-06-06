namespace BibliotecaApp.Etapa8.Contracts.Events;

public sealed record BookRegistered(Guid BookId, string Isbn, string Title, int InitialStock)
    : IntegrationEvent(Guid.NewGuid(), DateTime.UtcNow);

public sealed record BookAvailabilityChanged(Guid BookId, int AvailableStock)
    : IntegrationEvent(Guid.NewGuid(), DateTime.UtcNow);

public sealed record LoanCreated(Guid LoanId, Guid BookId, Guid UserId, DateTime DueDateUtc)
    : IntegrationEvent(Guid.NewGuid(), DateTime.UtcNow);

public sealed record LoanReturned(Guid LoanId, Guid BookId, Guid UserId, DateTime ReturnedAtUtc)
    : IntegrationEvent(Guid.NewGuid(), DateTime.UtcNow);

public sealed record UserSuspended(Guid UserId, string Reason)
    : IntegrationEvent(Guid.NewGuid(), DateTime.UtcNow);
