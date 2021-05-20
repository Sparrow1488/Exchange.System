using ExchangeSystem.Requests.Objects.Entities;
using System.Data.Entity;

namespace ExchangeServer.SQLDataBase
{
    public class UsersDbContext : DbContext
    {
        protected UsersDbContext() : base("DbConnectionString") { }
        public DbSet<User> Users { get; }
    }
}
