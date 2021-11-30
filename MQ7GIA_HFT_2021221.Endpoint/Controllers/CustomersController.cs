using Microsoft.AspNetCore.Mvc;
using MQ7GIA_HFT_2021221.Logic;
using MQ7GIA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Endpoint.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersLogic _customersLogic;

        public CustomersController(ICustomersLogic customersLogic)
        {
            _customersLogic = customersLogic;
        }

        [HttpGet("AllCustomers")] //works
        public IList<Customers> GetAllCustomersResult()
        {
            return _customersLogic.GetAllCustomers();
        }

        [HttpGet("{id}")] //works
        public Customers GetCustomerByIDResult(int id)
        {
            return _customersLogic.GetCustomerById(id);
        }

        
        [HttpPost("AddCustomer")] //works
        public void CreateCustomerResult(Customers customer)
        {
            _customersLogic.AddCustomer(customer.ContractId,customer.FirstName,customer.LastName,customer.Email,customer.PhoneNumber);
        }

        [HttpPut("ChangeEmail")] //works
        public void ChangeEmailResult(Customers customer)
        {
            _customersLogic.ChangeEmail(customer.CustomerID,customer.Email);
        }

        [HttpPut("ChangePhoneNumber")] //works
        public void ChangePhonenumberResult(Customers customer)
        {
            _customersLogic.ChangePhonenumber(customer.CustomerID, customer.PhoneNumber);
        }

        [HttpDelete("{id}")] //works
        public void DeleteCustomerResult(int id)
        {
            _customersLogic.DeleteCustomer(id);
        }
    }
}
