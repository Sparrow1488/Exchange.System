using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace ExchangeServer.SQLDataBase
{
    public class AccsDbContext : DbContext
    {
        public AccsDbContext() : base("DbConnectionString") { }
        public DbSet<UserPassport> Passports { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
