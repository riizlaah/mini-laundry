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
            base.OnModelCreating(modelBuilder);
        }
    }
}
