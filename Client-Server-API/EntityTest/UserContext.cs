using EntityTestLibraryFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace EntityTest
{
    public class UserContext : DbContext
    {
        public UserContext() : base("DbConnectionString")
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
