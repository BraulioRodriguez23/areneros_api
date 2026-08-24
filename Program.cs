using Areneros.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// El SpaProxy redirige la navegación inicial al dev server de Vite para tener HMR completo;
// ahí el fetch a la API queda cross-origin, de ahí el CORS (solo hace falta en desarrollo).
if (builder.Environment.IsDevelopment())
{
    var spaOrigin = builder.Configuration["Cors:SpaOrigin"]
        ?? throw new InvalidOperationException("Falta configurar Cors:SpaOrigin en appsettings.Development.json.");

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
            policy.WithOrigins(spaOrigin)
                  .AllowAnyHeader()
                  .AllowAnyMethod());
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// En desarrollo el flujo vive en http (SpaProxy redirige a Vite en http://localhost:5173,
// y VITE_API_BASE_URL apunta a http://localhost:5133). Forzar https aquí rompería ese fetch
// con un redirect cross-origin a un certificado de desarrollo no confiado.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseCors();
}

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
