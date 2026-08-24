using Areneros.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Areneros.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<LecturaArenero> LecturasArenero => Set<LecturaArenero>();
    public DbSet<LecturaHidro> LecturasHidro => Set<LecturaHidro>();

    // SQL Server (datetime2) no guarda zona horaria: al leer, EF Core devuelve
    // DateTime.Kind = Unspecified, lo que hace que el JSON salga sin "Z" y el
    // navegador lo malinterprete como hora local. Forzamos Kind = Utc al leer
    // (los valores guardados ya son UTC porque el frontend los manda así).
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var utcConverter = new ValueConverter<DateTime, DateTime>(
            v => v,
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(utcConverter);
                }
            }
        }
    }
}
