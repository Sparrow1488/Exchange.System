using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace EntityTest
{
    public class TestContext : DbContext
    {
        public TestContext() : base("DbConnectionString")
        {
        }
        public DbSet<Test> Tests { get; set; }
    }
}
