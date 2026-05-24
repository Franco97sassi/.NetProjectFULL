using BibliotecaApp.Etapa4.Application.Books;
using BibliotecaApp.Etapa4.Domain.Repositories;
using BibliotecaApp.Etapa4.Infrastructure.Data;
using BibliotecaApp.Etapa4.Infrastructure.Repositories;
using System.Text;
using BibliotecaApp.Etapa4.API.Filters;
using BibliotecaApp.Etapa4.API.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BibliotecaApp.Etapa4.API.Hubs;
using BibliotecaApp.Etapa4.API.Grpc;
using BibliotecaApp.Etapa4.API.Integrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BibliotecaDbContext>(x => x.UseInMemoryDatabase("BibliotecaEtapa4"));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<GlobalExceptionFilter>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateBookCommand).Assembly));
builder.Services.AddControllers(options => { options.Filters.Add<GlobalExceptionFilter>(); }); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddGrpc();
builder.Services.AddSignalR();
builder.Services.AddHttpClient<IOpenLibraryClient, OpenLibraryClient>(client =>
{
    client.BaseAddress = new Uri("https://openlibrary.org");
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "BibliotecaApp.Etapa4", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese: Bearer {token}"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var jwtKey = builder.Configuration["Jwt:Key"] ?? "super-secret-key-for-stage4-demo";
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGrpcService<BookGrpcService>();
app.MapHub<LibraryHub>("/hubs/library");
app.MapGet("/api/health", () => Results.Ok(new
{
    stage =6,
    architecture = "clean",
    previousStagesCovered = new[]
    {
       "Etapa3: persistencia con EF Core y servicios de consulta",
        "Etapa4: clean architecture, repositorio, unit of work, MediatR y CQRS",
        "Etapa6: SignalR, gRPC e integración con APIs de terceros"
    },
    status = "ok"
}));

app.MapGet("/api/books/minimal", async ([FromServices] IMediator mediator, CancellationToken ct) =>
    Results.Ok(await mediator.Send(new GetAllBooksQuery(), ct)))
    .RequireAuthorization();
app.Run();
