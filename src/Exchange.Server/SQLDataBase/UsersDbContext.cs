using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace ExchangeServer.SQLDataBase
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext() : base("DbConnectionString") { }
        public DbSet<UserPassport> Passports { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
