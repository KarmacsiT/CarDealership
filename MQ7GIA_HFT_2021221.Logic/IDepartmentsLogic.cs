using MQ7GIA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Logic
{
    public interface IDepartmentsLogic
    {
        void AddDepartment(int id, string name, string address);

        void ChangeAddress(int id, string newAddress);

        void DeleteDepartment(int id);

        IList<Departments> GetAllDepartments();
       
        Departments GetDepartmentById(int id);

        List<Cars> CarsOnThisDeparment(int departmentID);
    }
}
