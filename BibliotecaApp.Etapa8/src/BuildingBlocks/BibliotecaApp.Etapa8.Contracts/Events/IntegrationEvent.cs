namespace BibliotecaApp.Etapa8.Contracts.Events;

public abstract record IntegrationEvent(Guid EventId, DateTime OccurredOnUtc, int Version = 1);
