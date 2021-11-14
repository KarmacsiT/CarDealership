using MQ7GIA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Logic
{
    public interface IContractsLogic
    {
        void AddContract(int id, string type, string date, string expiryDate);

        void ChangeContractExpiryDate(int id, string newContractExpiryDate);

        void DeleteContract(int id);

        IList<Contracts> GetAllContracts();

        Contracts GetContractById(int id);

        List<Customers> CustomerOfCar(int id);
        DateTime? ContractExpireDateOfCustomer(int customerID);
        int TotalLeaseExpenditureForCustomer(int customerID);
    }
}
