using BibliotecaApp.Etapa4.Application.Books;
using BibliotecaApp.Etapa4.Domain.Repositories;
using BibliotecaApp.Etapa4.Infrastructure.Data;
using BibliotecaApp.Etapa4.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BibliotecaDbContext>(x => x.UseInMemoryDatabase("BibliotecaEtapa4"));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateBookCommand).Assembly));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/api/health", () => Results.Ok(new { stage = 4, architecture = "clean", status = "ok" }));

app.Run();
