using MQ7GIA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Logic
{
    public interface ICustomersLogic
    {
        void AddCustomer(int id, string firstName, string lastName, string email, long phoneNumber);

        void ChangeEmail(int id, string newEmail);

        void ChangePhonenumber(int id, long newPhonenumber);

        void DeleteCustomer(int id);
        
        IList<Customers> GetAllCustomers();

        Customers GetCustomerById(int id);

        List<Customers> CustomersBasedOnFuelType(string fuel_type);

    }
}
