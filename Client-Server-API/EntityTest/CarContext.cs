using EntityTestLibraryFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace EntityTest
{
    public class CarContext : DbContext
    {
        public CarContext() : base("DbConnectionString")
        {
        }
        public DbSet<Car> Cars { get; set; }
    }
}
