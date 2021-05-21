using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using System.Data.Entity;

namespace ExchangeServer.SQLDataBase
{
    public class AccountsDbContext : DbContext
    {
        protected AccountsDbContext() : base("DbConnectionString") { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPassport> Passports { get; set; }
    }
}
