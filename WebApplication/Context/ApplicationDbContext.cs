using Microsoft.EntityFrameworkCore;
using WebApp.Model;

namespace WebApp.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Persona> Personas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}

