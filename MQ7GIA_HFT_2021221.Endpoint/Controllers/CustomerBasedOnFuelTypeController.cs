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
    [Route("customers_extra")]
    public class CustomerBasedOnFuelTypeController : ControllerBase
    {
        private readonly ICustomersLogic _customersLogic;

        public CustomerBasedOnFuelTypeController(ICustomersLogic customersLogic)
        {
            _customersLogic = customersLogic;
        }

        [HttpGet("{fuel_type}")] //works
         public List<Customers> CustomersBasedOnFuelTypeResult(string fuel_type)
         {
                return _customersLogic.CustomersBasedOnFuelType(fuel_type);
         }
    }
}
