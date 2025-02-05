using DealershipApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DealershipApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) //base will shove all date up to dbcontext
        { 

        }

        //tell the context what are tables are below
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Dealership> Dealerships { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DealershipVehicle> DealershipVehicles { get; set; }

        //on model creating allows you to customize tables without going into db, you might want customizations in your code
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DealershipVehicle>()
                .HasKey(pc => new { pc.VehicleId, pc.DealershipId});

            modelBuilder.Entity<DealershipVehicle>()
                .HasOne(p => p.Vehicle)
                .WithMany(pc => pc.DealershipVehicles)
                .HasForeignKey(p => p.VehicleId);

            modelBuilder.Entity<DealershipVehicle>()
                .HasOne(p => p.Dealership)
                .WithMany(pc => pc.DealershipVehicles)
                .HasForeignKey(c => c.DealershipId);//should be different variable
        }

    }
}
//@8:14