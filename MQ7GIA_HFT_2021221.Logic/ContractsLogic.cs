using MQ7GIA_HFT_2021221.Models;
using MQ7GIA_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Logic
{
    public class ContractsLogic : IContractsLogic
    {
        IContractsRepository contractsRepository;
        ICustomersRepository customersRepository;
        ICarsRepository carsRepository;
       
        public ContractsLogic(IContractsRepository contractsRepo, ICustomersRepository customersRepo, ICarsRepository carsRepo)
        {
            contractsRepository = contractsRepo;
            customersRepository = customersRepo;
            carsRepository = carsRepo;
        }
        
        public void AddContract(int id, string type, string date, string expiryDate)
        {
            contractsRepository.AddContract(id, type, date, expiryDate);  
        }

        public void ChangeContractExpiryDate(int id, string newContractExpiryDate)
        {
            contractsRepository.ChangeContractExpiryDate(id, newContractExpiryDate);
        }

        public void DeleteContract(int id)
        {
            contractsRepository.DeleteContract(id);
        }

        public IList<Contracts> GetAllContracts()
        {
            return contractsRepository.GetAll().ToList();
        }

        public Contracts GetContractById(int id)
        {
            return contractsRepository.GetOne(id);
        }

        public List<Customers> CustomerOfCar(int CarID) // multitable
        {
            IQueryable<Contracts> AllContracts = GetAllContracts().AsQueryable();
            int SearchedCustomerID = AllContracts.Where(contract => contract.CarID == CarID).FirstOrDefault().CustomerID;

            return customersRepository.GetAll().Where(customer => customer.CustomerID == SearchedCustomerID).ToList();
        }
        
        public DateTime? ContractExpireDateOfCustomer(int customerID)
        {
            IList<Contracts> contracts = GetAllContracts();
            foreach (var contract in contracts)
            {
                if (contract.CustomerID == customerID)
                {
                    return contract.ContractExpiryDate;
                }
            }
            throw new Exception("There is no such CustomerID registered.");
        }

        //Helper function for calculating the difference in months between two DateTime 
        public static int LeaseMonthDifference(DateTime lease_end, DateTime lease_start)
        {
            return (lease_end.Month - lease_start.Month) + 12 * (lease_end.Year - lease_start.Year);
        }
        
        public int TotalLeaseExpenditureForCustomer(int customerID) //multitable
        {
            List<Contracts> AllContracts = GetAllContracts().ToList();
            Contracts SearchedContract = AllContracts.Where(contract => contract.CustomerID == customerID).FirstOrDefault();

            
            //Takes the number of mounths between the starting lease date and the lease expiry date and multiples it with the mounthly leasing price
            return LeaseMonthDifference(SearchedContract.ContractDate, (DateTime)SearchedContract.ContractExpiryDate) * carsRepository.GetOne(SearchedContract.CarID).LeasePrice;

        }

    }
}
