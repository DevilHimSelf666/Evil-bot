using Evil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Persistence
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<Dragon>  Dragons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($@"Filename=D:\workspace\discord\IkEvil\Evil.Infrastructure\Database\db.sqlite");
        }
    }
}
