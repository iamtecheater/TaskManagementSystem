using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tasks> Task { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmpInTask> TaskInTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DeptID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<EmpInTask>()
                .HasKey(e => new { e.EmployeeID, e.TaskID });

            modelBuilder.Entity<EmpInTask>()
                .HasOne(e => e.Employee)
                .WithMany(t => t.Task);

            modelBuilder.Entity<EmpInTask>()
                .HasOne(t => t.Task)
                .WithMany(e => e.Employees);
        }
    }
}
