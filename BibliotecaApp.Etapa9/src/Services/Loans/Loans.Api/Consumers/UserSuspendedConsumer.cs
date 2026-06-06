using BibliotecaApp.Etapa8.Contracts.Events;
using MassTransit;

namespace Loans.Api.Consumers;
public sealed class UserSuspendedConsumer(HashSet<Guid> suspendedUsers) : IConsumer<UserSuspended>
{
    public Task Consume(ConsumeContext<UserSuspended> context)
    {
        suspendedUsers.Add(context.Message.UserId);
        return Task.CompletedTask;
    }
}
