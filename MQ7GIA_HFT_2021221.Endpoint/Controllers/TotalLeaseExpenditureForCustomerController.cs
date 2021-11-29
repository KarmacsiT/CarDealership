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
    [Route("contracts_noncrud")]
    public class TotalLeaseExpenditureForCustomerController: ControllerBase
    {
        private readonly IContractsLogic _contractsLogic;

        public TotalLeaseExpenditureForCustomerController(IContractsLogic contractsLogic)
        {
            _contractsLogic = contractsLogic;
        }

        [HttpGet("{TotalLeaseExpenditureForCustomerID}")] //works
        public int TotalLeaseExpenditureForCustomerResult(int TotalLeaseExpenditureForCustomerID)
        {
            return _contractsLogic.TotalLeaseExpenditureForCustomer(TotalLeaseExpenditureForCustomerID);
        }
    }
}
