using Microsoft.EntityFrameworkCore;
using Notatnik.Shared.Models;

namespace Notatnik.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

    }
}
