using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using System.Data.Entity;

namespace ExchangeServer.SQLDataBase
{
    public class AccountsDbContext : DbContext
    {
        public AccountsDbContext() : base("DbConnectionString") { }
        public DbSet<UserPassport> Passports { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
