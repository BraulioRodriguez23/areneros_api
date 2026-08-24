using Areneros.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Configuración de CORS para aceptar peticiones desde el frontend (ahora que estarán separados en Vercel y Render).
var spaOrigin = builder.Configuration["Cors:SpaOrigin"]
    ?? Environment.GetEnvironmentVariable("CORS_ORIGIN") // Para ponerlo en Render
    ?? "*"; // Permitir todo temporalmente si no se configura, aunque es mejor configurarlo en Render

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        if (spaOrigin == "*")
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
        else
        {
            policy.WithOrigins(spaOrigin).AllowAnyHeader().AllowAnyMethod();
        }
    });
});

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

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
