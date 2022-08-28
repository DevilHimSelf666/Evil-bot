using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SqliteDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($@"Filename=D:\workspace\discord\IkEvil\Evil.Infrastructure\Database\db.sqlite");
        }
    }
}
