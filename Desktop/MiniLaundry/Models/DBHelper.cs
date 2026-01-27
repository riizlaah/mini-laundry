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
            modelBuilder.Entity<Job>().HasData(adminJob, new Job { Id = 2, Name = "Pencuci" }, new Job { Id = 3, Name = "Penyetrika" }, new Job { Id = 4, Name = "Kurir" });
            modelBuilder.Entity<Employee>().HasData(new Employee
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
            }, new Employee
            {
                Id = 2,
                Name = "Hartono",
                Email = "hartono09@gmail.com",
                Password = "p4s?",
                PhoneNum = "+62876533876532",
                Address = "Mars",
                DateOfBirth = new DateTime(2003, 8, 7),
                Salary = 2500000m,
                JobId = 2
            }, new Employee
            {
                Id = 3,
                Name = "Ubed",
                Email = "ubed25@gmail.com",
                Password = "p4s?",
                PhoneNum = "+6286544987320",
                Address = "Ds. Klewer, Kec. Tulis",
                DateOfBirth = new DateTime(2002, 5, 17),
                Salary = 2500000m,
                JobId = 3
            }, new Employee
            {
                Id = 4,
                Name = "Komar",
                Email = "komar@gmail.com",
                Password = "p4s?",
                PhoneNum = "+6289267530098",
                Address = "Ds. Sengon, Kec. Subah",
                DateOfBirth = new DateTime(2003, 8, 7),
                Salary = 2500000m,
                JobId = 4
            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = 1,
                Name = "Alok",
                PhoneNum = "+6256488976532",
                Address = "Batang"
            }, new Customer
            {
                Id = 2,
                Name = "Bowo",
                PhoneNum = "+6287655387653",
                Address = "Pekalongan"
            }, new Customer
            {
                Id = 3,
                Name = "Budiono",
                PhoneNum = "+6280766498076",
                Address = "Tegal"
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
            }, new Package
            {
                Id = 2,
                Name = "Paket Kilat",
                Description = "Paket yang mendekati kecepatan cahaya",
                Duration = 6,
                Price = 150000
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
            }, new DetailPackage
            {
                Id = 4,
                PackageId = 2,
                ServiceId = 3,
                TotalUnitService = 3
            }, new DetailPackage
            {
                Id = 5,
                PackageId = 2,
                ServiceId = 4,
                TotalUnitService = 3
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
