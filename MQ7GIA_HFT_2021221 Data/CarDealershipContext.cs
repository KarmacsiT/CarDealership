using Microsoft.EntityFrameworkCore;
using MQ7GIA_HFT_2021221_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221_Data
{
    class CarDealershipContext : DbContext
    {
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }


        public CarDealershipContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Data Source = (LocalDB)
                 \MSSQLLocalDB; AttachDbFilename =|DataDirectory|
                 \CarDatabase.mdf; Integrated Security = True");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cars>()
                      .HasOne(car => car.Department)
                      .WithMany(department => department.CarCollection)
                      .HasForeignKey(car => car.DepartmentID)
                      .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
