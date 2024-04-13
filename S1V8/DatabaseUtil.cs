using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1V8
{
    internal class DatabaseUtil : DbContext
    {
        internal DbSet<Employee> employee { get; set; }
        internal DbSet<Gender> gender { get; set; }
        internal DbSet<Designation> designation { get; set; }
        internal DbSet<Employeestatus> statusemployee { get; set; }

        //not compulsory to override this method, but it's often necessary
        //to customize the database schema to match your application's requirements.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee", "harvest");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.DOB).HasColumnType("date");
                entity.Property(e => e.Nic);
                entity.Property(e => e.Mobile);
                entity.Property(e => e.Email);
                entity.HasOne(e => e.Gender).WithMany(g => g.Employees).HasForeignKey(e => e.GenderId);
                entity.HasOne(e => e.Designation).WithMany(d => d.Employees).HasForeignKey(e => e.DesignationId);
                entity.HasOne(e => e.Employeestatus).WithMany(s => s.Employees).HasForeignKey(e => e.EmployeeStatusId);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("gender", "harvest");
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Id).ValueGeneratedOnAdd();
                entity.Property(g => g.Name).IsRequired();
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.ToTable("designation", "harvest");
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Id).ValueGeneratedOnAdd();
                entity.Property(d => d.Name).IsRequired();
            });

            modelBuilder.Entity<Employeestatus>(entity =>
            {
                entity.ToTable("statusemployee", "harvest");
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Id).ValueGeneratedOnAdd();
                entity.Property(s => s.Name).IsRequired();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost; uid=us2; pwd=abcd1234; database=harvest");
            }
        }
    }
}
