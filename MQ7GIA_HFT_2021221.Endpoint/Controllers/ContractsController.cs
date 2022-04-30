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
    [Route("contracts")]
    public class ContractsController : ControllerBase
    {
        IContractsLogic _contractsLogic;

        public ContractsController(IContractsLogic contractsLogic)
        {
            _contractsLogic = contractsLogic;
        }

        [HttpGet] //works Originally:HttpGet("AllContracts")
        public IList<Contracts> GetAllContractsResult()
        {
            return _contractsLogic.GetAllContracts();
        }

        [HttpGet("{id}")] //works
        public Contracts GetContractsByIDResult(int id)
        {
            return _contractsLogic.GetContractById(id);
        }

        [HttpGet("ContractExpireDateOfCustomer")]
        public DateTime? ContractExpireDateOfCustomerResult(int CustomerID)
        {
            return _contractsLogic.ContractExpireDateOfCustomer(CustomerID);
        }

        [HttpPost] //not working Originally: HttpPost("AddContract")
        public void CreateContractResult([FromBody] Contracts contract)
        {
            _contractsLogic.AddContract(contract.CarID, contract.ContractType, contract.ContractDate.ToString(), contract.ContractExpiryDate.ToString());
        }

        [HttpPut("ChangeContractExpiryDate")] //works
        public void ChangeContractExpiryDateResult(Contracts contract)
        {
            _contractsLogic.ChangeContractExpiryDate(contract.ContractID, contract.ContractExpiryDate.ToString());
        }

        [HttpDelete("{id}")] //works
        public void DeleteContractResult(int id)
        {
            _contractsLogic.DeleteContract(id);
        }
    }
}
