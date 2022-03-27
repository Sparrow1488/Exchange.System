using Exchange.System.Entities;
using System.Data.Entity;

namespace Exchange.Server.SQLDataBase
{
    public class PublicationsDbContext : DbContext
    {
        public PublicationsDbContext() : base("DbConnectionString") { }
        public DbSet<Publication> Publications { get; set; }
    }
}
