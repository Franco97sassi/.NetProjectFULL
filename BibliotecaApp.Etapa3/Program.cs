using System.Text;
using BibliotecaApp.Etapa3.Data;
using BibliotecaApp.Etapa3.Filters;
using BibliotecaApp.Etapa3.Middlewares;
using BibliotecaApp.Etapa3.Services;
using BibliotecaApp.Etapa3.Services.Reports;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

var dbProvider = builder.Configuration["Database:Provider"]?.ToLowerInvariant() ?? "inmemory";

builder.Services.AddDbContext<BibliotecaDbContext>(opt =>
{
    var sqlServerConnection = builder.Configuration.GetConnectionString("SqlServer");
    var postgresConnection = builder.Configuration.GetConnectionString("Postgres");

    switch (dbProvider)
    {
        case "sqlserver" when !string.IsNullOrWhiteSpace(sqlServerConnection):
            opt.UseSqlServer(sqlServerConnection);
            break;

        case "postgres" when !string.IsNullOrWhiteSpace(postgresConnection):
            opt.UseNpgsql(postgresConnection);
            break;

        default:
            opt.UseInMemoryDatabase("BibliotecaDb");
            break;
    }
});

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<GlobalExceptionFilter>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BibliotecaApp.Etapa3",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT"
    });

    options.AddSecurityRequirement(document =>
    {
        return new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecuritySchemeReference("Bearer"),
                new List<string>()
            }
        };
    });
});







var jwtKey = builder.Configuration["Jwt:Key"] ?? "super-secret-key-for-stage3-demo";
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

app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/api/health", () => Results.Ok(new
{
    status = "ok",
    stage = 3,
    dbProvider,
    timestamp = DateTime.UtcNow
}));

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<BibliotecaDbContext>();
db.Database.EnsureCreated();
BibliotecaSeeder.Seed(db);

app.Run();