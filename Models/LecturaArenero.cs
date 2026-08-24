namespace Areneros.Api.Models;

public class LecturaArenero
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public required string Planta { get; set; }
    public required string Separador { get; set; }
    public required string Tambor { get; set; }
    public required string Arenero { get; set; }
    public required string Estado { get; set; } // "Tapado" | "Destapado"
}
