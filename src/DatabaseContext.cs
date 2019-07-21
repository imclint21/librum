using Librum.Models;
using Microsoft.EntityFrameworkCore;

namespace Librum
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}