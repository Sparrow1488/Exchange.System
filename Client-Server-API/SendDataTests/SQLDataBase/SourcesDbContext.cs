using ExchangeSystem.Requests.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace ExchangeServer.SQLDataBase
{
    public class SourcesDbContext : DbContext
    {
        public SourcesDbContext() : base("DbConnectionString") { }
        public DbSet<Source> Sources { get; set; }
    }
}
