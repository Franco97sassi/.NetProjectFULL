using BibliotecaApp.Etapa8.Contracts.Events;
using MassTransit;
using Users.Api.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); builder.Services.AddEndpointsApiExplorer(); builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<List<UserItem>>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((_, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"] ?? "rabbitmq", "/", h => { h.Username("guest"); h.Password("guest"); });
    });
});

var app = builder.Build();
app.UseSwagger(); app.UseSwaggerUI();
app.MapControllers();
app.Run();
