using BibliotecaApp.Etapa8.Contracts.Events;
using MassTransit;
using Notifications.Worker.Consumers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<LoanCreatedConsumer>();
    x.AddConsumer<BookAvailabilityChangedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h => { h.Username("guest"); h.Password("guest"); });
        cfg.ConfigureEndpoints(context);
    });
});

await builder.Build().RunAsync();
