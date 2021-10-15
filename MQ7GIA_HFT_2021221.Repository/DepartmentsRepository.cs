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
    public class DepartmentsRepository : Repository<Departments>, IDepartmentsRepository
    {
        public DepartmentsRepository(CarDealershipContext cd_ctx) : base(cd_ctx) { /*Empty on purpose*/ }

        public void AddDepartment(int id, string name, string address) //In case the company wants to expand
        {
            cd_ctx.Departments.Add(new Departments
            {
                DepartmentID = id,
                DepartmentName = name,
                Address = address
            });

            cd_ctx.SaveChanges();
        }

        public void ChangeAddress(int id, string newAddress)
        {
            var Department = GetOne(id);
            Department.Address = newAddress;
            
            cd_ctx.SaveChanges();
        }

        public void DeleteDepartment(int id)
        {
            var DepartmentToDelete = cd_ctx.Departments.FirstOrDefault(x => x.DepartmentID == id);
            cd_ctx.Departments.Remove(DepartmentToDelete);

            cd_ctx.SaveChanges();
        }

        public override Departments GetOne(int id)
        {
            return GetAll().SingleOrDefault(item => item.DepartmentID == id);
        }
    }
}
