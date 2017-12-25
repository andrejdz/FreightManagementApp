namespace FreightManagement.ORM
{
    using System.Data.Entity;
    using FreightManagement.Domain.Model;
    using FreightManagement.ORM.Migrations;

    public class FreightManagementContext : DbContext
    {
        public FreightManagementContext() : base("name=FreightManagement")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FreightManagementContext, Configuration>("FreightManagement"));
        }

        public virtual DbSet<Truck> Trucks { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Cargo> Cargoes { get; set; }
    }
}