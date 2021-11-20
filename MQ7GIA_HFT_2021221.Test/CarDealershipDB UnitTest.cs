using Microsoft.EntityFrameworkCore;
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
                //Navigation Property
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
                //Navigation Property
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
                //Navigation Property 
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
                //Navigation Property
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
                //Navigation Property
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
                //Navigation Property
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

        [TestCase(1)]
        public void CustomerOfCar_GetsCustomerSpecifiedInParameter(int customerID)
        {
            Mock<IContractsRepository> contractsRepoMock = new Mock<IContractsRepository>();
            Mock<ICustomersRepository> customersRepoMock = new Mock<ICustomersRepository>();
            Mock<ICarsRepository> carsRepoMock = new Mock<ICarsRepository>();
            List<Contracts> ContractsFillerList = new List<Contracts>();
            List<Customers> CustomersFillerList = new List<Customers>();
            ContractsLogic contractsLogic = new ContractsLogic(contractsRepoMock.Object, customersRepoMock.Object,carsRepoMock.Object);
           
            #region Adding Mock Data
            Contracts con1 = new Contracts
            {
                ContractID = 1,
                ContractType = "Lease",
                ContractDate = DateTime.Parse("2020.05.12"),
                ContractExpiryDate = DateTime.Parse("2023.12.15"),
                //Foreign Key
                CarID = 1,
                CustomerID = 1,
            };

            Customers p1 = new Customers
            {
                CustomerID = 1,
                FirstName = "Test",
                LastName = "Person",
                Email = "place@holder.com",
                PhoneNumber = 16505130514,
                ContractID = 1,
                //Navigation Property
                Contract = con1
            };

            ContractsFillerList.Add(con1);
            CustomersFillerList.Add(p1);
            #endregion
            
            contractsRepoMock.Setup(x => x.GetAll()).Returns(ContractsFillerList.AsQueryable);
            customersRepoMock.Setup(x => x.GetAll()).Returns(CustomersFillerList.AsQueryable);

            Assert.That(contractsLogic.CustomerOfCar(customerID).FirstOrDefault().CustomerID, Is.EqualTo(customerID));
        }
    
        [TestCase(1)]
        public void TotalLeaseExpenditureForCustomer_CalculatesPriceCorrectly(int customerID)
        {
            Mock<IContractsRepository> contractsRepoMock = new Mock<IContractsRepository>();
            Mock<ICustomersRepository> customersRepoMock = new Mock<ICustomersRepository>();
            Mock<ICarsRepository> carsRepoMock = new Mock<ICarsRepository>();
            List<Contracts> ContractsFillerList = new List<Contracts>();
            ContractsLogic contractsLogic = new ContractsLogic(contractsRepoMock.Object, customersRepoMock.Object, carsRepoMock.Object);
           
            #region Adding Mock Data
            Contracts con1 = new Contracts
            {
                ContractID = 1,
                ContractType = "Lease",
                ContractDate = DateTime.Parse("2020.05.12"),
                ContractExpiryDate = DateTime.Parse("2020.07.12"),
                //Foreign Key
                CarID = 1,
                CustomerID = 1,
            };

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
                LeasePrice = 150000,
                SellingPrice = 23500000,
                //Foreign key
                DepartmentID = 1
            };

            Customers p1 = new Customers
            {
                CustomerID = 1,
                FirstName = "Test",
                LastName = "Person",
                Email = "place@holder.com",
                PhoneNumber = 16505130514,
                ContractID = 1,
                //Navigation Property
                Contract = con1
            };

            ContractsFillerList.Add(con1);
            #endregion
            
            contractsRepoMock.Setup(x => x.GetAll()).Returns(ContractsFillerList.AsQueryable);
            carsRepoMock.Setup(x => x.GetOne(customerID)).Returns(c1);
            
            Assert.That(contractsLogic.TotalLeaseExpenditureForCustomer(customerID) == c1.LeasePrice * 2, Is.True);
        }

       [TestCase("Banana")]
        public void CustomersBasedOnFuelType_ThrowsExceptionOnInvalidFuelTypes(string fuel_type)
        {
            Mock<IContractsRepository> contractsRepoMock = new Mock<IContractsRepository>();
            Mock<ICustomersRepository> customersRepoMock = new Mock<ICustomersRepository>();
            Mock<ICarsRepository> carsRepoMock = new Mock<ICarsRepository>();
            CustomersLogic customersLogic = new CustomersLogic(customersRepoMock.Object, carsRepoMock.Object, contractsRepoMock.Object);
            
            Assert.That(() => customersLogic.CustomersBasedOnFuelType(fuel_type), Throws.Exception);
        }

        [TestCase("Diesel")]
        public void CustomersBasedOnFuelType_Fetches_CustomersWithDieselCar(string fuel_type)
        {
            Mock<IContractsRepository> contractsRepoMock = new Mock<IContractsRepository>();
            Mock<ICustomersRepository> customersRepoMock = new Mock<ICustomersRepository>();
            Mock<ICarsRepository> carsRepoMock = new Mock<ICarsRepository>();
            List<Cars> CarsFillerList = new List<Cars>();
            List<Contracts> ContractsFillerList = new List<Contracts>();
            CustomersLogic customersLogic = new CustomersLogic(customersRepoMock.Object, carsRepoMock.Object, contractsRepoMock.Object);
           
            //There are 2 Diesel cars in the test data, that are in posession of CustomerID: 1 and 2
            #region Adding Mock Data 
            Cars c1 = new Cars
            {
                CarID = 1,
                CarBrand = "Some Brand",
                CarModell = "Some Diesel Modell 1",
                LicensePlate = "ASD-123",
                Warranty = null,
                EngineDisplacement = 1.9,
                FuelType = "Diesel",
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
                CarModell = "Some Diesel Modell 2",
                LicensePlate = "ASD-123",
                Warranty = 1,
                EngineDisplacement = 2.0,
                FuelType = "Diesel",
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
                CarModell = "Some Petrol Modell 1",
                LicensePlate = "ASD-123",
                Warranty = null,
                EngineDisplacement = 2.0,
                FuelType = "Petrol",
                HorsePower = 140,
                Transmission = "Automatic",
                Mileage = 52489,
                MOTUntil = DateTime.Parse("2022.03.30"),
                LeasePrice = 1500000,
                SellingPrice = 23500000,
                DepartmentID = 1
            };

            Cars c4 = new Cars
            {
                CarID = 4,
                CarBrand = "Some Brand",
                CarModell = "Some Electric Modell 1",
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

            Customers p1 = new Customers
            {
                CustomerID = 1,
                FirstName = "Test",
                LastName = "Person",
                Email = "place@holder.com",
                PhoneNumber = 16505130514,
                ContractID = 1,
                //Navigation Property
                Contract = con1
            };

            Customers p2 = new Customers
            {
                CustomerID = 2,
                FirstName = "Test",
                LastName = "Person",
                Email = "place@holder.com",
                PhoneNumber = 16505130514,
                ContractID = 2,
                //Navigation Property
                Contract = con2
            };
           
            CarsFillerList.Add(c1);
            CarsFillerList.Add(c2);
            CarsFillerList.Add(c3);
            CarsFillerList.Add(c4);
            ContractsFillerList.Add(con1);
            ContractsFillerList.Add(con2);
            ContractsFillerList.Add(con3);
            ContractsFillerList.Add(con4);
            #endregion

            carsRepoMock.Setup(x => x.GetAll()).Returns(CarsFillerList.AsQueryable);
            contractsRepoMock.Setup(x => x.GetAll()).Returns(ContractsFillerList.AsQueryable);
            customersRepoMock.Setup(x => x.GetOne(1)).Returns(p1);
            customersRepoMock.Setup(x => x.GetOne(2)).Returns(p2);

            Assert.That(customersLogic.CustomersBasedOnFuelType(fuel_type).FirstOrDefault().CustomerID == 1, Is.True);
            Assert.That(customersLogic.CustomersBasedOnFuelType(fuel_type).Last().CustomerID == 2, Is.True);
        }

        [Test]
        public void CarsRepository_AddCarsMethod_WorksAsIntended()
        {
            Mock<DbSet<Cars>> CarsDbSetMock = new Mock<DbSet<Cars>>();
            Mock<CarDealershipContext> ctx_Mock = new Mock<CarDealershipContext>();
            ctx_Mock.Setup(x => x.Cars).Returns(CarsDbSetMock.Object);
            CarsRepository carsRepository = new CarsRepository(ctx_Mock.Object);

            #region Filler Data
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
                LeasePrice = 150,
                SellingPrice = 23500000,
                //Foreign Key
                DepartmentID = 1
            };
            #endregion

            carsRepository.AddCar(c1.CarID, c1.CarBrand, c1.CarModell, c1.LicensePlate, c1.Warranty, c1.EngineDisplacement, c1.FuelType, c1.HorsePower, c1.Transmission, c1.Mileage, c1.MOTUntil.ToString(), c1.LeasePrice, c1.SellingPrice);

            CarsDbSetMock.Verify(x => x.Add(It.IsAny<Cars>()), Times.Once);
            ctx_Mock.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Test]
        public void ContractsRepository_AddContractMethod_WorksAsIntended()
        {
            Mock<DbSet<Contracts>> ContractsDbSetMock = new Mock<DbSet<Contracts>>();
            Mock<CarDealershipContext> ctx_Mock = new Mock<CarDealershipContext>();
            ctx_Mock.Setup(x => x.Contracts).Returns(ContractsDbSetMock.Object);
            ContractsRepository contractsRepository = new ContractsRepository(ctx_Mock.Object);

            #region Filler Data
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
            #endregion

            contractsRepository.AddContract(con1.ContractID, con1.ContractType, con1.ContractDate.ToString(), con1.ContractExpiryDate.ToString());

            ContractsDbSetMock.Verify(x => x.Add(It.IsAny<Contracts>()), Times.Once);
            ctx_Mock.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Test]
        public void CustomersRepository_AddCustomerMethod_WorksAsIntended()
        {
            Mock<DbSet<Customers>> CustomersDbSetMock = new Mock<DbSet<Customers>>();
            Mock<CarDealershipContext> ctx_Mock = new Mock<CarDealershipContext>();
            ctx_Mock.Setup(x => x.Customers).Returns(CustomersDbSetMock.Object);
            CustomersRepository customersRepository = new CustomersRepository(ctx_Mock.Object);

            #region Filler Data
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
            #endregion

            customersRepository.AddCustomer(p1.CustomerID, p1.FirstName, p1.LastName, p1.Email, p1.PhoneNumber);

            CustomersDbSetMock.Verify(x => x.Add(It.IsAny<Customers>()), Times.Once);
            ctx_Mock.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Test]
        public void DepartmentsRepository_AddDepartmentMethod_WorksAsIntended()
        {
            Mock<DbSet<Departments>> DepartmentsDbSetMock = new Mock<DbSet<Departments>>();
            Mock<CarDealershipContext> ctx_Mock = new Mock<CarDealershipContext>();
            ctx_Mock.Setup(x => x.Departments).Returns(DepartmentsDbSetMock.Object);
            DepartmentsRepository departmentsRepository = new DepartmentsRepository(ctx_Mock.Object);

            #region Filler Data
            Departments d1 = new Departments
            {
                DepartmentID = 1,
                DepartmentName = "LudicrousLease Landingcenter",
                Address = "2789 Business St. 21"
            };
            #endregion

            departmentsRepository.AddDepartment(d1.DepartmentID, d1.DepartmentName, d1.Address);

            DepartmentsDbSetMock.Verify(x => x.Add(It.IsAny<Departments>()), Times.Once);
            ctx_Mock.Verify(x => x.SaveChanges(), Times.Once());
        }
    }
}    

