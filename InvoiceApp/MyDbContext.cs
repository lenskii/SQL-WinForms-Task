using System;
using System.Data.Entity;

namespace InvoiceApp
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("DbConnectionString")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<MyDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Invoice> Invoice { get; set; }
    }
}
