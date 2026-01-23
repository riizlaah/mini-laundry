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
                @"Server=(localdb)\mssqllocaldb;Database=laundryApp;Integrated Security=True;")
                .UseSeeding((context, _) =>
                {
                    Job adminJob = new Job { Name = "Administrator" };
                    context.Set<Job>().Add(adminJob);
                    context.Set<Job>().Add(new Job { Name = "Pencuci" });
                    context.Set<Job>().Add(new Job { Name = "Penyetrika" });
                    context.Set<Employee>().Add(new Employee
                    {
                        Name = "Admin",
                        Email = "admin@penatu.id",
                        Password = "password",
                        PhoneNum = "089988776655",
                        Address = "Bumi",
                        DateOfBirth = new DateTime(2000, 1, 1),
                        Salary = 3000000,
                        Job = adminJob

                    });
                    context.SaveChanges();
                });
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasOne<Job>(e => e.Job).WithMany(j => j.Employees).HasForeignKey(e => e.JobId);
        }
    }
}
