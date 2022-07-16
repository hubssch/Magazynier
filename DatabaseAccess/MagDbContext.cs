using Microsoft.EntityFrameworkCore;

namespace Magazynier.DatabaseAccess
{
    public class MagDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "magazyno.db");
            optionsBuilder.UseSqlite($"Filename={path}");
        }

        public DbSet<DbDocument> Documents { get; set; }
        public DbSet<DbItem> Items { get; set; }
        public DbSet<DbArticle> Articles { get; set; }
        public DbSet<DbContract> Contracts { get; set; }
    }
}
