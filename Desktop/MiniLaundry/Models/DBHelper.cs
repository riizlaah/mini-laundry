using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MiniLaundry.Models
{
    public class DBHelper : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=laundryApp;Integrated Security=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Job adminJob = new Job { Id = 1, Name = "Administrator" };
            modelBuilder.Entity<Job>().HasData(adminJob, new Job { Id = 2, Name = "Pencuci" }, new Job { Id = 3, Name = "Penyetrika" });
            modelBuilder.Entity<Employee>().HasData(new
            {
                Id = 1,
                Name = "Admin",
                Email = "admin@penatu.id",
                Password = "p4s?",
                PhoneNum = "+6289988776655",
                Address = "Bumi",
                DateOfBirth = new DateTime(2000, 1, 1),
                Salary = 3000000m,
                JobId = adminJob.Id
            });
            modelBuilder.Entity<Unit>().HasData(new Unit { Id = 1, Name = "Kg" }, new Unit { Id = 2, Name = "m" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Kiloan" }, new Category { Id = 2, Name = "Meteran" });
            modelBuilder.Entity<Service>().HasData(new Service
            {
                Id = 1,
                CategoryId = 1,
                UnitId = 1,
                Name = "Cuci Kiloan",
                Price = 20000,
                EstimationDuration = 4
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
