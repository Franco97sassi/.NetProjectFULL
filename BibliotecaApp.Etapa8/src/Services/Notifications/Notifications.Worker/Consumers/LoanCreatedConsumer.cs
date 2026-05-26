using BibliotecaApp.Etapa8.Contracts.Events;
using MassTransit;

namespace Notifications.Worker.Consumers;
public sealed class LoanCreatedConsumer(ILogger<LoanCreatedConsumer> logger) : IConsumer<LoanCreated>
{
    public Task Consume(ConsumeContext<LoanCreated> context)
    {
        logger.LogInformation("LoanCreated notification: LoanId={LoanId}, UserId={UserId}", context.Message.LoanId, context.Message.UserId);
        return Task.CompletedTask;
    }
}
