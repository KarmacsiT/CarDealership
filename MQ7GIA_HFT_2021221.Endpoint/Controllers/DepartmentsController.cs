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
    [Route("departments")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsLogic _departmentsLogic;

        public DepartmentsController(IDepartmentsLogic departmentsLogic)
        {
            _departmentsLogic = departmentsLogic;
        }

        [HttpGet] //works origially:HttpGet("AllDepartments")
        public IList<Departments> GetAllDepartmentsResult()
        {
            return _departmentsLogic.GetAllDepartments();
        }

        [HttpGet("{id}")] //works
        public Departments GetDepartmentByIDResult(int id)
        {
            return _departmentsLogic.GetDepartmentById(id);
        }

        [HttpPost] //works originally: HttpPost("AddDepartment")
        public void CreateDepartmentResult(Departments department)
        {
            _departmentsLogic.AddDepartment(department.DepartmentID, department.DepartmentName, department.Address);
        }

        [HttpPut] //works originally:HttpPut("ChangeAddress")
        public void ChangeAddressResult(Departments department)
        {
            _departmentsLogic.ChangeAddress(department.DepartmentID, department.Address);
        }

        [HttpDelete("{id}")] //works
        public void DeleteDepartmentResult(int id)
        {
            _departmentsLogic.DeleteDepartment(id);
        }
    }
}
