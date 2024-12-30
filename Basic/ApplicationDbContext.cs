using System.Diagnostics.CodeAnalysis;
using Basic.Entities;
using Microsoft.EntityFrameworkCore;

namespace Basic
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext([ NotNullAttribute ] DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        // Name of the table
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
    }
}