using Exchange.System.Entities;
using System.Data.Entity;

namespace Exchange.Server.Database
{
    public class LettersDbContext : DbContext
    {
        public LettersDbContext() : base("DbConnectionString") { }
        public DbSet<Letter> Letters { get; set; }
    }
}
