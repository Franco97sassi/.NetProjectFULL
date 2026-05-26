using BibliotecaApp.Etapa8.Contracts.Events;
using MassTransit;

namespace Notifications.Worker.Consumers;
public sealed class BookAvailabilityChangedConsumer(ILogger<BookAvailabilityChangedConsumer> logger) : IConsumer<BookAvailabilityChanged>
{
    public Task Consume(ConsumeContext<BookAvailabilityChanged> context)
    {
        logger.LogInformation("BookAvailabilityChanged: BookId={BookId}, Available={Available}", context.Message.BookId, context.Message.AvailableStock);
        return Task.CompletedTask;
    }
}
