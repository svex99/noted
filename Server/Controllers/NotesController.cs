using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Noted.Server.Data;
using Noted.Server.Models;

namespace Noted.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly NotesContext _context;
    private readonly ILogger<NotesController> _logger;

    public NotesController(NotesContext context, ILogger<NotesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<Note[]>> ListNotes()
    {
        var notesList = await _context.Notes.ToListAsync();

        return Ok(notesList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Note>> RetrieveNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note == null)
        {
            return NotFound();
        }

        return note;
    }

    [HttpPost]
    public async Task<ActionResult<Note>> CreateNote(Note note)
    {
        await _context.Notes.AddAsync(note);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(RetrieveNote), new { id = note.Id }, note);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Note>> DeleteNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note != null)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }
}
