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
    public class ContractsRepository : Repository<Contracts>, IContractsRepository
    {
        public ContractsRepository(CarDealershipContext cd_ctx) : base(cd_ctx) { /*Empty on purpose*/ }

        public void AddContract(int id, string type, string date, string expiryDate)
        {
            cd_ctx.Contracts.Add(new Contracts
            {
                ContractID = id,
                ContractType = type,
                ContractDate = DateTime.Parse(date),
                ContractExpiryDate = DateTime.Parse(expiryDate)
            });
            
            cd_ctx.SaveChanges();
        }

        public void ChangeContractExpiryDate(int id, string newContractExpiryDate) //In case the client extends the lease
        {
            var Contract = GetOne(id);
            Contract.ContractExpiryDate = DateTime.Parse(newContractExpiryDate);
            
            cd_ctx.SaveChanges();
        }

        public void DeleteContract(int id)
        {
            var ContractToDelete = cd_ctx.Contracts.FirstOrDefault(x => x.ContractID == id);

            cd_ctx.Contracts.Remove(ContractToDelete);
            cd_ctx.SaveChanges();
        }

        public override Contracts GetOne(int id)
        {
            return GetAll().SingleOrDefault(item => item.ContractID == id);
        }
    }
}
