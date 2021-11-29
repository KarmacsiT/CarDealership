using Microsoft.EntityFrameworkCore;
using MQ7GIA_HFT_2021221.Models;
using MQ7GIA_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Logic
{
    public class CarsLogic : ICarsLogic
    {
        ICarsRepository carsRepository;
        IContractsRepository contractsRepository;
        ICustomersRepository customersRepository;

        //Dependency Injection
        public CarsLogic(ICarsRepository carsRepo, IContractsRepository contractsRepo, ICustomersRepository customersRepo)
        {
            carsRepository = carsRepo;
            contractsRepository = contractsRepo;
            customersRepository = customersRepo;
        }

        public void AddCar(int id, string brand, string modell, string licensePlate, int warranty, double engineDisplacement, string fuelType, int horsePower, string transmission, int mileage, string motUntil, int leasePrice, int sellingPrice)
        {
            carsRepository.AddCar(id,brand, modell, licensePlate, warranty, engineDisplacement, fuelType, horsePower, transmission, mileage, motUntil, leasePrice, sellingPrice);
        }

        public void ChangeMOT(int id, string newMOT)
        {
            carsRepository.ChangeMOT(id, newMOT);
        }

        public void ChangeNumericData(int id, string valueType, int newValue)
        {
            carsRepository.ChangeNumericData(id, valueType, newValue);
        }

        public void DeleteCar(int id)
        {
            carsRepository.DeleteCar(id);
        }

        public IList<Cars> GetAllCars()
        {
            return carsRepository.GetAll().ToList();
        }

        public Cars GetCarById(int id)
        {
            return carsRepository.GetOne(id);  
        }
        
        public List<Customers> CustomersWithoutWarranty() //multitable
        {
           List<Customers> SearchedCustomers = customersRepository.GetAll().Where(customer => customer.Contract.Car.Warranty == null).ToList();

            return SearchedCustomers;
        }
    }
}
