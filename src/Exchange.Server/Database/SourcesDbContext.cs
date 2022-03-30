using Exchange.System.Entities;
using System.Data.Entity;

namespace Exchange.Server.Database
{
    public class SourcesDbContext : DbContext
    {
        public SourcesDbContext() : base("DbConnectionString") { }
        public DbSet<Source> Sources { get; set; }
    }
}
