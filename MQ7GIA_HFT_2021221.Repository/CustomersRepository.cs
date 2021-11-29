using Microsoft.EntityFrameworkCore;
using MQ7GIA_HFT_2021221.Data;
using MQ7GIA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Repository
{
    public class CustomersRepository : Repository<Customers>, ICustomersRepository
    {
        public CustomersRepository(CarDealershipContext cd_ctx) : base(cd_ctx) { /*Empty on purpose*/ }

        public void AddCustomer(int id, string firstName, string lastName, string email, long phoneNumber)
        {
            cd_ctx.Customers.Add(new Customers
            { 
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                ContractId = id
            });

            cd_ctx.SaveChanges();
        }

        public void ChangeEmail(int id, string newEmail)
        {
            var Customer = GetOne(id);
            Customer.Email = newEmail;
            
            cd_ctx.SaveChanges();
        }

        public void ChangePhonenumber(int id, long newPhonenumber)
        {
            var Customer = GetOne(id);
            Customer.PhoneNumber = newPhonenumber;
            
            cd_ctx.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var CustomerToDelete = cd_ctx.Customers.FirstOrDefault(x => x.CustomerID == id);
            cd_ctx.Customers.Remove(CustomerToDelete);
            
            cd_ctx.SaveChanges();
        }

        public override Customers GetOne(int id)
        {
            return GetAll().SingleOrDefault(item => item.CustomerID == id);
        }
    }
}
