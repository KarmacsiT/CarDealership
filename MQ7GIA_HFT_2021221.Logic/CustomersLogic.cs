using MQ7GIA_HFT_2021221.Models;
using MQ7GIA_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Logic
{
    public class CustomersLogic : ICustomersLogic
    {
        ICustomersRepository customersRepository;
        ICarsRepository carsRepository;
        IContractsRepository contractsRepository;
        public CustomersLogic(ICustomersRepository customersRepo, ICarsRepository carsRepo, IContractsRepository contractsRepo)
        {
            customersRepository = customersRepo;
            carsRepository = carsRepo;
            contractsRepository = contractsRepo;
        }
        public void AddCustomer(int id, string firstName, string lastName, string email, long phoneNumber)
        {
            customersRepository.AddCustomer(id, firstName, lastName, email, phoneNumber);
        }

        public void ChangeEmail(int id, string newEmail)
        {
            customersRepository.ChangeEmail(id, newEmail);
        }

        public void ChangePhonenumber(int id, long newPhonenumber)
        {
            customersRepository.ChangePhonenumber(id, newPhonenumber);
        }

        public void DeleteCustomer(int id)
        {
            customersRepository.DeleteCustomer(id);
        }

        public IList<Customers> GetAllCustomers()
        {
            return customersRepository.GetAll().ToList();
        }

        public Customers GetCustomerById(int id)
        {
            return customersRepository.GetOne(id);
        }

        public List<Customers> CustomersBasedOnFuelType(string fuel_type) //multitable
        {
             
            List<Cars> AllCars = carsRepository.GetAll().ToList();
            List<Contracts> AllContracts = contractsRepository.GetAll().ToList();
            
            List<Cars> FilteredCars = new List<Cars>();
            List<Contracts> FilteredContracts = new List<Contracts>();
            List<Customers> FilteredCustomers = new List<Customers>();
            
            string[] ValidFuelTypes = { "PETROL", "DIESEL", "ELECTRIC" };
            
                //Checking whether fuel type input was correct
            if (ValidFuelTypes.Contains(fuel_type.ToUpper()))
            {
                //Getting the cars that runs on the specified fuel
                FilteredCars = AllCars.Where(car => car.FuelType.ToLower() == fuel_type).ToList();

                //Fetching Contracts that belongs to the filtered cars
                foreach (var car in FilteredCars)
                {
                    foreach (var contract in AllContracts)
                    {
                        if (car.CarID == contract.CarID)
                        {
                            FilteredContracts.Add(contract);
                        }

                    }

                }

                //Adding Customers based on the Filteredcontracts CustomerID
                foreach (var MatchingContract in FilteredContracts)
                {
                    FilteredCustomers.Add(GetCustomerById(MatchingContract.CustomerID));
                }

                return FilteredCustomers;

            }

            else
            {
                throw new Exception("There is no such fuel type.");
            }
            
        }
    }
}
