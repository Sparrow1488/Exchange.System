using ExchangeSystem.Requests.Objects.Entities;
using System.Data.Entity;

namespace ExchangeServer.SQLDataBase
{
    public class PublicationsDbContext : DbContext
    {
        public PublicationsDbContext() : base("DbConnectionString") { }
        public DbSet<Publication> Publications { get; set; }
    }
}
