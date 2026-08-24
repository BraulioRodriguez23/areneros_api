namespace Areneros.Api.Models;

public class LecturaHidro
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public required string Planta { get; set; }
    public required string Bateria { get; set; }
    public required string Hidrociclon { get; set; }
    public required string Estado { get; set; } // "Operando" | "No operando"
}
