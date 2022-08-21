using IkEvil.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static IkEvil.Models.Dragon;

namespace IkEvil.Repository
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<Dragon> Dragons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($@"Filename=D:\workspace\discord\IkEvil\IkEvil\App_Data\db.sqlite");
        }
    }
}
