using BibliotecaApp.Etapa8.Contracts.Events;
using Loans.Api.Consumers;
using Loans.Api.Models;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); builder.Services.AddEndpointsApiExplorer(); builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<List<LoanItem>>();
builder.Services.AddSingleton<HashSet<Guid>>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserSuspendedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"] ?? "rabbitmq", "/", h => { h.Username("guest"); h.Password("guest"); });
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();
app.UseSwagger(); app.UseSwaggerUI();
app.MapControllers();
app.Run();
