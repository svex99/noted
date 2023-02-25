using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Noted.Server.Models;

namespace Noted.Server.Data;

public class NotesContext : DbContext
{
    public DbSet<Note> Notes { get; set; }

    public NotesContext(DbContextOptions<NotesContext> options) : base(options)
    {
    }
}
