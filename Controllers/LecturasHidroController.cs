using Areneros.Api.Data;
using Areneros.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Areneros.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LecturasHidroController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LecturaHidro>>> Get([FromQuery] DateTime? desde, [FromQuery] DateTime? hasta)
    {
        var query = db.LecturasHidro.AsQueryable();
        if (desde is not null) query = query.Where(l => l.Fecha >= desde);
        if (hasta is not null) query = query.Where(l => l.Fecha <= hasta);
        return await query.OrderByDescending(l => l.Fecha).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] List<LecturaHidro> lecturas)
    {
        if (lecturas.Count == 0) return BadRequest("No hay lecturas para guardar.");
        db.LecturasHidro.AddRange(lecturas);
        await db.SaveChangesAsync();
        return Ok(new { guardadas = lecturas.Count });
    }
}
