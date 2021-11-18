using Microsoft.EntityFrameworkCore;
using MQ7GIA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Data
{
    public class CarDealershipContext : DbContext
    {
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }

        public CarDealershipContext()
        {
            this.Database?.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Data Source = (LocalDB)\MSSQLLocalDB; 
                 AttachDbFilename = |DataDirectory|\CarDealershipDB.mdf;
                 Integrated Security = True");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Data Seeding  

            Cars c1 = new Cars
            {
                CarID = 1,
                CarBrand = "Tesla",
                CarModell = "Model Y",
                LicensePlate = "MON-322",
                Warranty = 3,
                EngineDisplacement = null,
                FuelType = "Electric",
                HorsePower = 140,
                Transmission = "Automatic",
                Mileage = 52489,
                MOTUntil = DateTime.Parse("2022.03.30"),
                LeasePrice = 1500000,
                SellingPrice = 23500000,
                //Foreign Key
                DepartmentID = 1
            };

            Cars c2 = new Cars
            {
                CarID = 2,
                CarBrand = "Seat",
                CarModell = "Leon",
                LicensePlate = "SUN-732",
                Warranty = 2,
                EngineDisplacement = 2.0,
                FuelType = "Petrol",
                HorsePower = 250,
                Transmission = "Automatic",
                Mileage = 62321,
                MOTUntil = DateTime.Parse("2021.12.02"),
                LeasePrice = 200000,
                SellingPrice = 10000000,
                //Foreign Key
                DepartmentID = 2
            };

            Cars c3 = new Cars
            {
                CarID = 3,
                CarBrand = "Nissan",
                CarModell = "Leaf",
                LicensePlate = "ECO-420",
                Warranty = 2,
                EngineDisplacement = null, //Electric motors have no displacement
                FuelType = "Electric",
                HorsePower = 147,
                Transmission = "Automatic",
                Mileage = 30000,
                MOTUntil = null,    //MOT expired
                LeasePrice = 155000,
                SellingPrice = 6700000,
                //Foreign Key
                DepartmentID = 1
            };

            Cars c4 = new Cars
            {
                CarID = 4,
                CarBrand = "BMW",
                CarModell = "X6",
                LicensePlate = "POS-368",
                Warranty = null,            //Warranty expired
                EngineDisplacement = 2.0,
                FuelType = "Diesel",
                HorsePower = 286,
                Transmission = "Automatic",
                Mileage = 85321,
                MOTUntil = DateTime.Parse("2022.01.20"),
                LeasePrice = 250000,
                SellingPrice = 15000000,
                //Foreign Key
                DepartmentID = 2
            };

            Cars c5 = new Cars
            {
                CarID = 5,
                CarBrand = "Ford",
                CarModell = "Mustang",
                LicensePlate = "CEO-500",
                Warranty = 4,
                EngineDisplacement = 5.0,
                FuelType = "Petrol",
                HorsePower = 420,
                Transmission = "Automatic",
                Mileage = 10000,
                MOTUntil = DateTime.Parse("2022.05.20"),
                LeasePrice = 270000,
                SellingPrice = 16000000,
                //Foreign Key
                DepartmentID = 1
            };

            Cars c6 = new Cars
            {
                CarID = 6,
                CarBrand = "Skoda",
                CarModell = "Fabia",
                LicensePlate = "NAH-247",
                Warranty = 1,
                EngineDisplacement = 1.2,
                FuelType = "Petrol",
                HorsePower = 110,
                Transmission = "Manual",
                Mileage = 69852,
                MOTUntil = DateTime.Parse("2021.10.10"),
                LeasePrice = 90000,
                SellingPrice = 3000000,
                //Foreign Key
                DepartmentID = 2
            };

            Cars c7 = new Cars
            {
                CarID = 7,
                CarBrand = "Audi",
                CarModell = "A5",
                LicensePlate = "FAM-696",
                Warranty = 1,
                EngineDisplacement = 3.0,
                FuelType = "Diesel",
                HorsePower = 240,
                Transmission = "Automatic",
                Mileage = 84563,
                MOTUntil = DateTime.Parse("2022.02.08"),
                LeasePrice = 190000,
                SellingPrice = 9000000,
                //Foreign Key
                DepartmentID = 1
            };

            Cars c8 = new Cars
            {
                CarID = 8,
                CarBrand = "Hyundai",
                CarModell = "I30N",
                LicensePlate = "JAP-147",
                Warranty = 1,
                EngineDisplacement = 2.0,
                FuelType = "Petrol",
                HorsePower = 275,
                Transmission = "Manual",
                Mileage = 47516,
                MOTUntil = DateTime.Parse("2021.11.11"),
                LeasePrice = 195000,
                SellingPrice = 9300000,
                //Foreign Key
                DepartmentID = 2
            };

            Cars c9 = new Cars
            {
                CarID = 9,
                CarBrand = "Suzuki",
                CarModell = "Vitara",
                LicensePlate = "ZOK-159",
                Warranty = 6,
                EngineDisplacement = 1.4,
                FuelType = "Petrol",
                HorsePower = 140,
                Transmission = "Automatic",
                Mileage = 67451,
                MOTUntil = DateTime.Parse("2021.12.30"),
                LeasePrice = 150000,
                SellingPrice = 6000000,
                //Foreign Key
                DepartmentID = 1
            };

            Cars c10 = new Cars
            {
                CarID = 10,
                CarBrand = "Volkswagen",
                CarModell = "Passat",
                LicensePlate = "PRK-696",
                Warranty = 1,
                EngineDisplacement = 2.0,
                FuelType = "Diesel",
                HorsePower = 150,
                Transmission = "Manual",
                Mileage = 160000,
                MOTUntil = DateTime.Parse("2022.02.15"),
                LeasePrice = 160000,
                SellingPrice = 7000000,
                //Foreign Key
                DepartmentID = 2
            };



            Customers p1 = new Customers
            {
                CustomerID = 1,
                FirstName = "Elon",
                LastName = "Musk",
                Email = "dodgecoin@teslamotors.com",
                PhoneNumber = 16505130514,
                //Foreign Key
                ContractID = 1
            };

            Customers p2 = new Customers
            {
                CustomerID = 2,
                FirstName = "Bill",
                LastName = "Baller",
                Email = "corporate@bs.com",
                PhoneNumber = 18962358544,
                //Foreign Key
                ContractID = 2
            };

            Customers p3 = new Customers
            {
                CustomerID = 3,
                FirstName = "Lorem",
                LastName = "Ipsum",
                Email = "dolor@sit_amet.com",
                PhoneNumber = 13052478786,
                //Foreign Key
                ContractID = 3
            };

            Customers p4 = new Customers
            {
                CustomerID = 4,
                FirstName = "Karen",
                LastName = "Commander",
                Email = "speaking@manager.com",
                PhoneNumber = 16964767780,
                //Foreign Key
                ContractID = 4
            };

            Customers p5 = new Customers
            {
                CustomerID = 5,
                FirstName = "Fred",
                LastName = "Fratboy",
                Email = "free@beer.io",
                PhoneNumber = 16487205302,
                //Foreign Key
                ContractID = 5
            };

            Customers p6 = new Customers
            {
                CustomerID = 6,
                FirstName = "Sally",
                LastName = "Spiritual",
                Email = "yoga@mania.com",
                PhoneNumber = 17154801567,
                //Foreign Key
                ContractID = 6
            };

            Customers p7 = new Customers
            {
                CustomerID = 7,
                FirstName = "Paula",
                LastName = "Poser",
                Email = "follow@insta.com",
                PhoneNumber = 18187827055,
                //Foreign Key
                ContractID = 7
            };

            Customers p8 = new Customers
            {
                CustomerID = 8,
                FirstName = "Simon",
                LastName = "Sideways",
                Email = "fullsend@drift.com",
                PhoneNumber = 12414558332,
                //Foreign Key
                ContractID = 8
            };

            Customers p9 = new Customers
            {
                CustomerID = 9,
                FirstName = "Chad",
                LastName = "Charming",
                Email = "shees@bro.com",
                PhoneNumber = 12279330598,
                //Foreign Key
                ContractID = 9
            };

            Customers p10 = new Customers
            {
                CustomerID = 10,
                FirstName = "Chris",
                LastName = "Cosmic",
                Email = "supersonic@speed.com",
                PhoneNumber = 12093581825,
                //Foreign Key
                ContractID = 10
            };


            Departments d1 = new Departments
            {
                DepartmentID = 1,
                DepartmentName = "LudicrousLease Landingcenter",
                Address = "2789 Business St. 21"
            };

            Departments d2 = new Departments
            {
                DepartmentID = 2,
                DepartmentName = "SavySellers Solution",
                Address = "2789 Market St. 58"
            };


            Contracts con1 = new Contracts
            {
                ContractID = 1,
                ContractType = "Lease",
                ContractDate = DateTime.Parse("2020.05.12"),
                ContractExpiryDate = DateTime.Parse("2023.12.15"),
                //Foreign Key
                CustomerID = 1,
                CarID = 1
            };

            Contracts con2 = new Contracts
            {
                ContractID = 2,
                ContractType = "Sell",
                ContractDate = DateTime.Parse("2020.05.12"),
                ContractExpiryDate = null,
                //Foreign Key
                CustomerID = 2,
                CarID = 2
            };

            Contracts con3 = new Contracts
            {
                ContractID = 3,
                ContractType = "Lease",
                ContractDate = DateTime.Parse("2021.06.08"),
                ContractExpiryDate = DateTime.Parse("2023.04.02"),
                //Foreign Key
                CustomerID = 3,
                CarID = 3
            };

            Contracts con4 = new Contracts
            {
                ContractID = 4,
                ContractType = "Sell",
                ContractDate = DateTime.Parse("2020.01.17"),
                ContractExpiryDate = null,
                //Foreign Key
                CustomerID = 4,
                CarID = 4
            };

            Contracts con5 = new Contracts
            {
                ContractID = 5,
                ContractType = "Lease",
                ContractDate = DateTime.Parse("2019.05.07"),
                ContractExpiryDate = DateTime.Parse("2020.02.20"),
                //Foreign Key
                CustomerID = 5,
                CarID = 5
            };

            Contracts con6 = new Contracts
            {
                ContractID = 6,
                ContractType = "Sell",
                ContractDate = DateTime.Parse("2021.10.08"),
                ContractExpiryDate = null,
                //Foreign Key
                CustomerID = 6,
                CarID = 6
            };

            Contracts con7 = new Contracts
            {
                ContractID = 7,
                ContractType = "Lease",
                ContractDate = DateTime.Parse("2021.09.01"),
                ContractExpiryDate = DateTime.Parse("2021.12.15"),
                //Foreign Key
                CustomerID = 7,
                CarID = 7
            };

            Contracts con8 = new Contracts
            {
                ContractID = 8,
                ContractType = "Sell",
                ContractDate = DateTime.Parse("2021.10.21"),
                ContractExpiryDate = null,
                //Foreign Key
                CustomerID = 8,
                CarID = 8
            };

            Contracts con9 = new Contracts
            {
                ContractID = 9,
                ContractType = "Lease",
                ContractDate = DateTime.Parse("2018.07.17"),
                ContractExpiryDate = DateTime.Parse("2022.11.25"),
                //Foreign Key
                CustomerID = 9,
                CarID = 9
            };

            Contracts con10 = new Contracts
            {
                ContractID = 10,
                ContractType = "Sell",
                ContractDate = DateTime.Parse("2017.01.30"),
                ContractExpiryDate = null,
                //Foreign Key
                CustomerID = 10,
                CarID = 10
            };

            #endregion
            //--------------------------------------------------------------//

            con1.CarID = c1.CarID;
            con2.CarID = c2.CarID;
            con3.CarID = c3.CarID;
            con4.CarID = c4.CarID;
            con5.CarID = c5.CarID;
            con6.CarID = c6.CarID;
            con7.CarID = c7.CarID;
            con8.CarID = c8.CarID;
            con9.CarID = c9.CarID;
            con10.CarID = c10.CarID;

            con1.CustomerID = p1.CustomerID;
            con2.CustomerID = p2.CustomerID;
            con3.CustomerID = p3.CustomerID;
            con4.CustomerID = p4.CustomerID;
            con5.CustomerID = p5.CustomerID;
            con6.CustomerID = p6.CustomerID;
            con7.CustomerID = p7.CustomerID;
            con8.CustomerID = p8.CustomerID;
            con9.CustomerID = p9.CustomerID;
            con10.CustomerID = p10.CustomerID;

            c1.DepartmentID = d1.DepartmentID;
            c2.DepartmentID = d2.DepartmentID;
            c3.DepartmentID = d1.DepartmentID;
            c4.DepartmentID = d2.DepartmentID;
            c5.DepartmentID = d1.DepartmentID;
            c6.DepartmentID = d2.DepartmentID;
            c7.DepartmentID = d1.DepartmentID;
            c8.DepartmentID = d2.DepartmentID;
            c9.DepartmentID = d1.DepartmentID;
            c10.DepartmentID = d2.DepartmentID;


            //--------------------------------------------------------------//

          //Not needed
            /* modelBuilder.Entity<Cars>()
                          .HasOne(car => car.Department)
                          .WithMany(department => department.CarCollection)
                          .HasForeignKey(car => car.DepartmentID)
                          .OnDelete(DeleteBehavior.ClientSetNull;*/

            modelBuilder.Entity<Cars>().HasData(c1, c2, c3, c4, c5, c6, c7, c8, c9, c10);
            modelBuilder.Entity<Contracts>().HasData(con1, con2, con3, con4, con5, con6, con7, con8, con9, con10);
            modelBuilder.Entity<Customers>().HasData(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
            modelBuilder.Entity<Departments>().HasData(d1, d2);

        }
    }
}
