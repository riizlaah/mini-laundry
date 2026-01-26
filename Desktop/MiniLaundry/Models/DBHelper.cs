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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<DetailPackage> DetailPackages { get; set; }
        public DbSet<DetailTransaction> DetailTransactions { get; set; }
        public DbSet<HeaderTransaction> HeaderTransactions { get; set; }
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
            modelBuilder.Entity<Customer>().HasData(new
            {
                Id = 1,
                Name = "Alok",
                PhoneNum = "+6256488976532",
                Address = "Batang"
            }, new
            {
                Id = 2,
                Name = "Bowo",
                PhoneNum = "+6287655387653",
                Address = "Pekalongan"
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
            }, new Service
            {
                Id = 2,
                CategoryId = 1,
                UnitId = 1,
                Name = "Cuci Setrika",
                Price = 30000,
                EstimationDuration = 6
            }, new Service
            {
                Id = 3,
                CategoryId = 1,
                UnitId = 1,
                Name = "Cuci Kilat",
                Price = 30000,
                EstimationDuration = 1
            }, new Service
            {
                Id = 4,
                CategoryId = 1,
                UnitId = 1,
                Name = "Setrika Kilat",
                Price = 20000,
                EstimationDuration = 1
            }, new Service
            {
                Id = 5,
                CategoryId = 2,
                UnitId = 2,
                Name = "Cuci Korden",
                Price = 10000,
                EstimationDuration = 2
            });
            modelBuilder.Entity<Package>().HasData(new Package
            {
                Id = 1,
                Name = "Paket Hari Raya",
                Description = "Paket untuk hari raya",
                Duration = 14,
                Price = 100000
            });
            modelBuilder.Entity<DetailPackage>().HasData(new DetailPackage
            {
                Id = 1,
                PackageId = 1,
                ServiceId = 1,
                TotalUnitService = 1
            }, new DetailPackage
            {
                Id = 2,
                PackageId = 1,
                ServiceId = 2,
                TotalUnitService = 2
            }, new DetailPackage
            {
                Id = 3,
                PackageId = 1,
                ServiceId = 5,
                TotalUnitService = 2
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
