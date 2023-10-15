using Microsoft.EntityFrameworkCore;
using ServiceManagerFire.Models;

namespace ServiceManagerFire.Data
{
    public class FireContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Objekt> Objekts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RoleManager> RoleManagers { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        public FireContext()
        {
  //          Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=dbFire;Trusted_Connection=True;"); }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RoleManager>().HasData(
                    new RoleManager { Id = 1, Name = "админ" },
                    new RoleManager { Id = 2, Name = "руководитель" },
                    new RoleManager { Id = 3, Name = "исполнитель" });
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "admin", Password = "admin", RoleManagerId = 1 });


        }
  
        
        
    }

    
}
