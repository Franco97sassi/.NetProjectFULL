using BibliotecaApp.Etapa8.Contracts.Events;
using Catalog.Api.Consumers;
using Catalog.Api.Models;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<List<BookItem>>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"] ?? "redis:6379";
    options.InstanceName = "biblioteca:catalog:";
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<LoanCreatedConsumer>();
    x.AddConsumer<LoanReturnedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"] ?? "rabbitmq", "/", h =>
        {
            h.Username(builder.Configuration["RabbitMq:Username"] ?? "guest");
            h.Password(builder.Configuration["RabbitMq:Password"] ?? "guest");
        });
        cfg.ConfigureEndpoints(context);
    });
});
builder.Services.AddMemoryCache();
builder.Services.AddResponseCompression();
var app = builder.Build();
app.UseSwagger(); app.UseSwaggerUI();
app.MapControllers();
app.Run();
