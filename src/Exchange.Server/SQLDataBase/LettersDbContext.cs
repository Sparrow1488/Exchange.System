using ExchangeSystem.Requests.Objects.Entities;
using System.Data.Entity;

namespace ExchangeServer.SQLDataBase
{
    public class LettersDbContext : DbContext
    {
        public LettersDbContext() : base("DbConnectionString") { }
        public DbSet<Letter> Letters { get; set; }
    }
}
