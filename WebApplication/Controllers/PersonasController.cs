using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.Model;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonasController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
    {
        return await context.Personas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Persona>> GetPersona(string id)
    {
        var persona = await context.Personas.FindAsync(id);

        if (persona == null)
        {
            return NotFound(new { Message = "Persona no encontrada" });
        }

        return persona!;
    }

    [HttpPost]
    public async Task<ActionResult<Persona>> PostPersona(Persona persona)
    {
        if (await context.Personas.AnyAsync(p => p.Id == persona.Id))
        {
            return BadRequest(new { Message = "El ID ya existe. Usa un ID único." });
        }

        context.Personas.Add(persona);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetPersona", new { id = persona.Id }, persona);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPersona(string id, Persona persona)
    {
        if (id != persona.Id)
        {
            return BadRequest(new { Message = "El ID de la ruta no coincide con el del cuerpo de la solicitud." });
        }

        context.Entry(persona).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PersonaExists(id))
            {
                return NotFound(new { Message = "Persona no encontrada para actualizar." });
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersona(string id)
    {
        var persona = await context.Personas.FindAsync(id);
        if (persona == null)
        {
            return NotFound(new { Message = "Persona no encontrada para eliminar." });
        }

        context.Personas.Remove(persona);
        await context.SaveChangesAsync();

        return Ok(new { Message = "Persona eliminada con éxito." });
    }

    private bool PersonaExists(string id)
    {
        return context.Personas.Any(e => e.Id == id);
    }
}
