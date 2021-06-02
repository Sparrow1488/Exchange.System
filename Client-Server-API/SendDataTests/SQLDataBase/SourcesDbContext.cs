using ExchangeSystem.Requests.Objects.Entities;
using System.Data.Entity;

namespace ExchangeServer.SQLDataBase
{
    public class SourcesDbContext : DbContext
    {
        public SourcesDbContext() : base("DbConnectionString") { }
        public DbSet<Source> Sources { get; set; }
    }
}
