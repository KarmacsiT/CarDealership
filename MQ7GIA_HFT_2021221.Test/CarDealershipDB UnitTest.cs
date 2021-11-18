using Moq;
using MQ7GIA_HFT_2021221.Data;
using MQ7GIA_HFT_2021221.Logic;
using MQ7GIA_HFT_2021221.Models;
using MQ7GIA_HFT_2021221.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Test
{
    [TestFixture]
    class CarDealershipDB_UnitTest
    {

        [Test]
        public void CustomersWithoutWarranty_GetsTheRightAmountofCustomers()
        {
            //Arrange
            Mock<ICarsRepository> carsrepomock = new Mock<ICarsRepository>();
            Mock<IContractsRepository> contractsrepomock = new Mock<IContractsRepository>();
            Mock<ICustomersRepository> customerrepomock = new Mock<ICustomersRepository>();
            List<Cars> CarsDummyList = new List<Cars>();
            List<Contracts> ContractsDummyList = new List<Contracts>();
            List<Customers> CustomersDummyList = new List<Customers>();

            //Look out for comments in Adding Data
            //Adding Data has 2 Customers without warranty
            #region Adding Data
            Cars c1 = new Cars
            {
                CarID = 1,
                CarBrand = "Some Brand",
                CarModell = "Some Modell",
                LicensePlate = "ASD-123",
                Warranty = null,
                EngineDisplacement = null,
                FuelType = "Electric",
                HorsePower = 140,
                Transmission = "Automatic",
                Mileage = 52489,
                MOTUntil = DateTime.Parse("2022.03.30"),
                LeasePrice = 1500000,
                SellingPrice = 23500000,
                DepartmentID = 1
            };

            Cars c2 = new Cars
            {
                CarID = 2,
                CarBrand = "Some Brand",
                CarModell = "Some Modell",
                LicensePlate = "ASD-123",
                Warranty = 1,
                EngineDisplacement = null,
                FuelType = "Electric",
                HorsePower = 140,
                Transmission = "Automatic",
                Mileage = 52489,
                MOTUntil = DateTime.Parse("2022.03.30"),
                LeasePrice = 1500000,
                SellingPrice = 23500000,
                DepartmentID = 2
            };

            Cars c3 = new Cars
            {
                CarID = 3,
                CarBrand = "Some Brand",
                CarModell = "Some Modell",
                LicensePlate = "ASD-123",
                Warranty = null,
                EngineDisplacement = null,
                FuelType = "Electric",
                HorsePower = 140,
                Transmission = "Automatic",
                Mileage = 52489,
                MOTUntil = DateTime.Parse("2022.03.30"),
                LeasePrice = 1500000,
                SellingPrice = 23500000,
                DepartmentID = 1
            };

            Contracts con1 = new Contracts
            {
                ContractID = 1,
                ContractType = "Lease",
                ContractDate = DateTime.Parse("2020.05.12"),
                ContractExpiryDate = DateTime.Parse("2023.12.15"),
                //Foreign Key
                CarID = 1,
                CustomerID = 1,
                //Navigation Property (totaly bad practice)
                Car = c1

            };

            Contracts con2 = new Contracts
            {
                ContractID = 2,
                ContractType = "Sell",
                ContractDate = DateTime.Parse("2020.05.12"),
                ContractExpiryDate = DateTime.Parse("2023.12.15"),
                //Foreign Key
                CarID = 2,
                CustomerID = 2,
                //Navigation Property (totaly bad practice)
                Car = c2
            };

            Contracts con3 = new Contracts
            {
                ContractID = 3,
                ContractType = "Lease",
                ContractDate = DateTime.Parse("2020.05.12"),
                ContractExpiryDate = DateTime.Parse("2023.08.15"),
                //Foreign Key
                CarID = 3,
                CustomerID = 3,
                //Navigation Property (totaly bad practice)
                Car = c3
            };

            Customers p1 = new Customers
            {
                CustomerID = 1,
                FirstName = "Test",
                LastName = "Person",
                Email = "place@holder.com",
                PhoneNumber = 16505130514,
                ContractID = 1,
                //Navigation Property (totaly bad practice)
                Contract = con1
            };

            Customers p2 = new Customers
            {
                CustomerID = 2,
                FirstName = "Place",
                LastName = "Holder",
                Email = "place@holder.com",
                PhoneNumber = 18962358544,
                ContractID = 2,
                //Navigation Property (totaly bad practice)
                Contract = con2
            };

            Customers p3 = new Customers
            {
                CustomerID = 3,
                FirstName = "Lorem",
                LastName = "Ipsum",
                Email = "dolor@sit_amet.com",
                PhoneNumber = 13052478786,
                ContractID = 3,
                //Navigation Property (totaly bad practice)
                Contract = con3
            };

            con1.CustomerID = p1.CustomerID;
            con2.CustomerID = p2.CustomerID;
            con3.CustomerID = p3.CustomerID;

            con1.CarID = c1.CarID;
            con2.CarID = c2.CarID;
            con3.CarID = c3.CarID;

            CarsDummyList.Add(c1);
            CarsDummyList.Add(c2);
            CarsDummyList.Add(c3);
            ContractsDummyList.Add(con1);
            ContractsDummyList.Add(con2);
            ContractsDummyList.Add(con3);
            CustomersDummyList.Add(p1);
            CustomersDummyList.Add(p2);
            CustomersDummyList.Add(p3);
            #endregion

            carsrepomock.Setup(x => x.GetAll()).Returns(CarsDummyList.AsQueryable());
            contractsrepomock.Setup(x => x.GetAll()).Returns(ContractsDummyList.AsQueryable());
            customerrepomock.Setup(x => x.GetAll()).Returns(CustomersDummyList.AsQueryable());
            CarsLogic carsLogic = new CarsLogic(carsrepomock.Object, contractsrepomock.Object, customerrepomock.Object);

            //Act + Assert
            Assert.That(carsLogic.CustomersWithoutWarranty().Count() == 2, Is.True);
        }
        
        [TestCase(1)]
        public void CarsOnThisDepartment_IsNotEmpty(int DepartmentID)
        {
            Mock<ICarsRepository> carsRepoMock = new Mock<ICarsRepository>();
            Mock<IDepartmentsRepository> departmentsRepoMock = new Mock<IDepartmentsRepository>();
            List<Cars> CarsFillerList = new List<Cars>();
            DepartmentsLogic departmentsLogic = new DepartmentsLogic(departmentsRepoMock.Object, carsRepoMock.Object);

            #region Adding Mock Data
            Cars c1 = new Cars
            {
                CarID = 1,
                CarBrand = "Some Brand",
                CarModell = "Some Modell",
                LicensePlate = "ASD-123",
                Warranty = null,
                EngineDisplacement = null,
                FuelType = "Electric",
                HorsePower = 140,
                Transmission = "Automatic",
                Mileage = 52489,
                MOTUntil = DateTime.Parse("2022.03.30"),
                LeasePrice = 1500000,
                SellingPrice = 23500000,
                //Foreign key
                DepartmentID = 1
            };

            Cars c2 = new Cars
            {
                CarID = 2,
                CarBrand = "Some Brand",
                CarModell = "Some Modell",
                LicensePlate = "ASD-123",
                Warranty = 1,
                EngineDisplacement = null,
                FuelType = "Electric",
                HorsePower = 140,
                Transmission = "Automatic",
                Mileage = 52489,
                MOTUntil = DateTime.Parse("2022.03.30"),
                LeasePrice = 1500000,
                SellingPrice = 23500000,
                //Foreign key
                DepartmentID = 2
            };

            Cars c3 = new Cars
            {
                CarID = 3,
                CarBrand = "Some Brand",
                CarModell = "Some Modell",
                LicensePlate = "ASD-123",
                Warranty = null,
                EngineDisplacement = null,
                FuelType = "Electric",
                HorsePower = 140,
                Transmission = "Automatic",
                Mileage = 52489,
                MOTUntil = DateTime.Parse("2022.03.30"),
                LeasePrice = 1500000,
                SellingPrice = 23500000,
                //Foreign key
                DepartmentID = 1
            };

            Departments d1 = new Departments
            {
                DepartmentID = 1,
                DepartmentName = "Placeholder Department",
                Address = "Placeholder Str. 404"
            };

            CarsFillerList.Add(c1);
            CarsFillerList.Add(c2);
            CarsFillerList.Add(c3);
            #endregion

            carsRepoMock.Setup(x => x.GetAll()).Returns(CarsFillerList.AsQueryable);

            Assert.That(departmentsLogic.CarsOnThisDeparment(DepartmentID), Is.Not.Empty);
        }
    }    
}
