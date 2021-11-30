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
    [Route("CustomerOfCar")]
    public class CustomerOfCarController
    {
        private readonly IContractsLogic _contractsLogic;

        public CustomerOfCarController(IContractsLogic contractsLogic)
        {
            _contractsLogic = contractsLogic;
        }

        [HttpGet("{CustomerOfCarID}")] //works
        public List<Customers> CustomerOfCarResult(int CustomerOfCarID)
        {
            return _contractsLogic.CustomerOfCar(CustomerOfCarID);
        }
    }
}
