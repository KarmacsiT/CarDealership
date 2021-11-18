using MQ7GIA_HFT_2021221.Models;
using MQ7GIA_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Logic
{
    public class DepartmentsLogic : IDepartmentsLogic
    {
        IDepartmentsRepository departmentsRepository;
        ICarsRepository carsRepository;
        public DepartmentsLogic(IDepartmentsRepository departmentRepo, ICarsRepository carsRepo)
        {
            departmentsRepository = departmentRepo;
            carsRepository = carsRepo;
        }
        public void AddDepartment(int id, string name, string address)
        {
            departmentsRepository.AddDepartment(id, name, address);
        }

        public void ChangeAddress(int id, string newAddress)
        {
            departmentsRepository.ChangeAddress(id, newAddress);
        }

        public void DeleteDepartment(int id)
        {
            departmentsRepository.DeleteDepartment(id);
        }

        public IList<Departments> GetAllDepartments()
        {
            return departmentsRepository.GetAll().ToList();
        }

        public Departments GetDepartmentById(int id)
        {
            return departmentsRepository.GetOne(id);
        }
        public List<Cars> CarsOnThisDeparment(int departmentID) //multitable
        {
            List<Cars> AllCars = carsRepository.GetAll().ToList();

            return AllCars.Where(car => car.DepartmentID == departmentID).ToList();
        }
    }
}
